import tkinter as tk
from tkinter import ttk, filedialog
import json
import os
from datetime import datetime, timedelta

# --- Configuration Loader ---
def load_config():
    """Loads rates, allowances, and multipliers from config.json, or builds safe defaults."""
    config_file = "config.json"
    
    # Internal defaults mirroring your original workplace parameters
    default_config = {
        "BASE_RATE": 10.00,
        "LAUNDRY_ALLOWANCE_PER_SHIFT": 0.00,
        "MULTIPLIERS": {
            "Base Day (Mon-Fri 7am-6pm)": 1.00,
            "Evening (Mon-Fri 6pm-11pm)": 1.00,
            "Saturday (7am-11pm)": 1.00,
            "Sunday Day (9am-11pm)": 1.00,
            "Night Shift (11pm-7am Mon-Sat)": 1.00,
            "Sunday Peak/Night": 1.00
        }
    }
    
    if not os.path.exists(config_file):
        with open(config_file, 'w') as f:
            json.dump(default_config, f, indent=4)
        return default_config
        
    try:
        with open(config_file, 'r') as f:
            return json.load(f)
    except Exception:
        return default_config

# Initialize global configuration variables
CONFIG = load_config()
BASE_RATE = CONFIG["BASE_RATE"]
LAUNDRY_ALLOWANCE_PER_SHIFT = CONFIG["LAUNDRY_ALLOWANCE_PER_SHIFT"]
MULTIPLIERS = CONFIG["MULTIPLIERS"]

weekly_shifts = []

# --- Core Pay Logic ---
def calculate_shift_pay(day_name, start_str, end_str):
    """Calculates gross pay for a single shift, automatically splitting penalties."""
    fmt = "%H:%M"
    try:
        start_time = datetime.strptime(start_str, fmt)
        end_time = datetime.strptime(end_str, fmt)
    except ValueError:
        return -1, {}

    if end_time <= start_time:
        end_time += timedelta(days=1)

    current_time = start_time
    shift_earnings = 0.0
    
    # Dynamically build breakdown skeleton using keys mapped from JSON
    breakdown = {key: 0.0 for key in MULTIPLIERS.keys()}

    time_delta = timedelta(minutes=15)
    hours_per_step = 0.25

    while current_time < end_time:
        current_day = day_name.lower()
        if (current_time - start_time).days > 0:
            days_of_week = ["monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday"]
            next_index = (days_of_week.index(current_day) + 1) % 7
            current_day = days_of_week[next_index]

        hour = current_time.hour + (current_time.minute / 60.0)

        # Map current step to the correct target time band label
        if current_day == "sunday":
            if 9.0 <= hour < 23.0:
                band = "Sunday Day (9am-11pm)"
            else:
                band = "Sunday Peak/Night"
        elif current_day == "saturday":
            if 7.0 <= hour < 23.0:
                band = "Saturday (7am-11pm)"
            else:
                band = "Night Shift (11pm-7am Mon-Sat)"
        else:
            if 7.0 <= hour < 18.0:
                band = "Base Day (Mon-Fri 7am-6pm)"
            elif 18.0 <= hour < 23.0:
                band = "Evening (Mon-Fri 6pm-11pm)"
            else:
                band = "Night Shift (11pm-7am Mon-Sat)"

        # Safe dictionary fallback lookup for multiplier targets
        multiplier = MULTIPLIERS.get(band, 1.0)
        rate = BASE_RATE * multiplier
        
        shift_earnings += rate * hours_per_step
        if band in breakdown:
            breakdown[band] += hours_per_step

        current_time += time_delta

    return shift_earnings, breakdown

# --- GUI Event Handlers ---
def add_shift():
    status_label.config(text="", fg="black")
    day = day_var.get()
    start = start_entry.get().strip()
    end = end_entry.get().strip()
    
    pay, breakdown = calculate_shift_pay(day, start, end)
    
    if pay == -1:
        status_label.config(text="Error: Use HH:MM format (e.g. 15:30)", fg="red")
        return
    
    weekly_shifts.append({"day": day, "start": start, "end": end, "pay": pay, "breakdown": breakdown})
    status_label.config(text=f"Success: Added {day} shift (${pay:.2f})", fg="green")
    
    start_entry.delete(0, tk.END)
    end_entry.delete(0, tk.END)
    update_totals()

def clear_shifts():
    weekly_shifts.clear()
    status_label.config(text="All shifts cleared.", fg="black")
    update_totals()

def save_roster_file():
    if not weekly_shifts:
        status_label.config(text="Error: No shifts to save.", fg="red")
        return
    
    file_path = filedialog.asksaveasfilename(
        defaultextension=".json",
        filetypes=[("JSON Files", "*.json"), ("All Files", "*.*")],
        title="Save Weekly Roster"
    )
    if file_path:
        try:
            with open(file_path, "w") as f:
                json.dump(weekly_shifts, f, indent=4)
            status_label.config(text="Roster saved successfully!", fg="green")
        except Exception as e:
            status_label.config(text=f"Error saving: {str(e)}", fg="red")

def load_roster_file():
    file_path = filedialog.askopenfilename(
        filetypes=[("JSON Files", "*.json"), ("All Files", "*.*")],
        title="Load Weekly Roster"
    )
    if file_path:
        try:
            with open(file_path, "r") as f:
                loaded_data = json.load(f)
            
            weekly_shifts.clear()
            for item in loaded_data:
                pay, breakdown = calculate_shift_pay(item["day"], item["start"], item["end"])
                weekly_shifts.append({
                    "day": item["day"], "start": item["start"], "end": item["end"], 
                    "pay": pay, "breakdown": breakdown
                })
            
            status_label.config(text="Roster loaded successfully!", fg="green")
            update_totals()
        except Exception as e:
            status_label.config(text=f"Error loading: {str(e)}", fg="red")

def update_totals():
    total_hours_pay = 0.0
    total_shifts_count = len(weekly_shifts)
    
    weekly_brackets = {key: 0.0 for key in MULTIPLIERS.keys()}
    
    shifts_list_text = []
    for idx, shift in enumerate(weekly_shifts, 1):
        total_hours_pay += shift["pay"]
        shifts_list_text.append(f"Shift {idx}: {shift['day']} ({shift['start']} - {shift['end']}) -> ${shift['pay']:.2f}")
        for bracket, hours in shift["breakdown"].items():
            if bracket in weekly_brackets:
                weekly_brackets[bracket] += hours
    
    if shifts_list_text:
        shifts_list_display.config(text="\n".join(shifts_list_text))
    else:
        shifts_list_display.config(text="No shifts added yet.")
        
    # Dynamically apply the dynamic allowance figure loaded from the file
    laundry_allowance = total_shifts_count * LAUNDRY_ALLOWANCE_PER_SHIFT
    final_gross = total_hours_pay + laundry_allowance
    
    shift_count_label.config(text=f"Shifts Logged: {total_shifts_count}")
    laundry_label.config(text=f"Laundry Allowance: ${laundry_allowance:.2f}")
    total_pay_label.config(text=f"TOTAL ESTIMATED PAY: ${final_gross:.2f}")
    
    breakdown_lines = []
    for bracket, hours in weekly_brackets.items():
        if hours > 0:
            breakdown_lines.append(f"{bracket}: {hours:.2f} hrs")
    
    if breakdown_lines:
        breakdown_display.config(text="\n".join(breakdown_lines))
    else:
        breakdown_display.config(text="No hours logged yet.")
    
    scroll_frame.update_idletasks()
    canvas.config(scrollregion=canvas.bbox("all"))

# --- GUI Window Setup ---
root = tk.Tk()
root.title("Weekly Pay Estimator")
root.geometry("480x600")

scrollbar = tk.Scrollbar(root)
scrollbar.pack(side="right", fill="y")

canvas = tk.Canvas(root, yscrollcommand=scrollbar.set, highlightthickness=0)
canvas.pack(side="left", fill="both", expand=True)
scrollbar.config(command=canvas.yview)

scroll_frame = tk.Frame(canvas)
canvas.create_window((0, 0), window=scroll_frame, anchor="nw", width=460)

tk.Label(scroll_frame, text="Weekly Casual Pay Estimator", font=("Arial", 14, "bold")).pack(pady=5)

# Dynamically prints configuration-sourced BASE_RATE on setup string
tk.Label(scroll_frame, text=f"Rate Profile: (Base: ${BASE_RATE:.4f}/hr)", font=("Arial", 10, "italic")).pack()

input_frame = tk.LabelFrame(scroll_frame, text=" Shift Input Details ", padx=15, pady=10)
input_frame.pack(pady=10, fill="x", padx=20)

tk.Label(input_frame, text="Select Day of Week:").pack(anchor="w")
day_var = tk.StringVar(value="Monday")
day_select = ttk.Combobox(input_frame, textvariable=day_var, state="readonly")
day_select['values'] = ("Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday")
day_select.pack(fill="x", pady=5)

tk.Label(input_frame, text="Start Time (24h format HH:MM):").pack(anchor="w")
start_entry = tk.Entry(input_frame)
start_entry.pack(fill="x", pady=5)

tk.Label(input_frame, text="End Time (24h format HH:MM):").pack(anchor="w")
end_entry = tk.Entry(input_frame)
end_entry.pack(fill="x", pady=5)

btn_frame1 = tk.Frame(scroll_frame)
btn_frame1.pack(pady=5)
tk.Button(btn_frame1, text="Add Shift", command=add_shift, width=12).pack(side="left", padx=5)
tk.Button(btn_frame1, text="Clear All Week", command=clear_shifts, width=12).pack(side="left", padx=5)

btn_frame2 = tk.Frame(scroll_frame)
btn_frame2.pack(pady=5)
tk.Button(btn_frame2, text="Save Roster", command=save_roster_file, width=12).pack(side="left", padx=5)
tk.Button(btn_frame2, text="Load Roster", command=load_roster_file, width=12).pack(side="left", padx=5)

status_label = tk.Label(scroll_frame, text="", font=("Arial", 10))
status_label.pack(pady=5)

shifts_frame = tk.LabelFrame(scroll_frame, text=" Added Shifts List ", padx=15, pady=10)
shifts_frame.pack(pady=10, fill="x", padx=20)
shifts_list_display = tk.Label(shifts_frame, text="No shifts added yet.", justify="left", font=("Arial", 9))
shifts_list_display.pack(anchor="w")

breakdown_frame = tk.LabelFrame(scroll_frame, text=" Total Hours Breakdown ", padx=15, pady=10)
breakdown_frame.pack(pady=10, fill="x", padx=20)

breakdown_display = tk.Label(breakdown_frame, text="No hours logged yet.", justify="left", font=("Arial", 9))
breakdown_display.pack(anchor="w")
calc_frame = tk.LabelFrame(scroll_frame, text=" Financial Calculation ", padx=15, pady=10)
calc_frame.pack(pady=10, fill="x", padx=20)
shift_count_label = tk.Label(calc_frame, text="Shifts Logged: 0", font=("Arial", 10))
shift_count_label.pack(anchor="w")
laundry_label = tk.Label(calc_frame, text=f"Laundry Allowance: $0.00", font=("Arial", 10))
laundry_label.pack(anchor="w")
total_pay_label = tk.Label(calc_frame, text="TOTAL ESTIMATED PAY: $0.00", font=("Arial", 12, "bold"))
total_pay_label.pack(anchor="w", pady=5)
scroll_frame.update_idletasks()
canvas.config(scrollregion=canvas.bbox("all"))

root.mainloop()
