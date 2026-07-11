import json
import os
from datetime import datetime, timedelta

# --- Configuration Loader ---
def load_config():
    """Loads rates and multipliers from config.json, or creates a default one."""
    config_file = "config.json"
    
    default_config = {
        "BASE_RATE": 13.4900,
        "LAUNDRY_ALLOWANCE_PER_SHIFT": 1.25,
        "MULTIPLIERS": {
            "Base Day (Mon-Fri 7am-6pm)": 1.25,
            "Evening (Mon-Fri 6pm-11pm)": 1.50,
            "Saturday (7am-11pm)": 1.50,
            "Sunday Day (9am-11pm)": 1.75,
            "Night Shift (11pm-7am Mon-Sat)": 1.75,
            "Sunday Peak/Night": 2.25
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
        print("Warning: Could not parse config.json. Using default profiles.")
        return default_config

# Load global configurations
CONFIG = load_config()
BASE_RATE = CONFIG["BASE_RATE"]
LAUNDRY_ALLOWANCE_PER_SHIFT = CONFIG["LAUNDRY_ALLOWANCE_PER_SHIFT"]
MULTIPLIERS = CONFIG["MULTIPLIERS"]

def calculate_shift_pay(day_name, start_str, end_str):
    """Calculates gross pay for a single shift, automatically splitting penalties."""
    fmt = "%H:%M"
    try:
        start_time = datetime.strptime(start_str, fmt)
        end_time = datetime.strptime(end_str, fmt)
    except ValueError:
        print("Error: Invalid time format. Please use HH:MM (e.g., 15:30).")
        return 0, {}

    if end_time <= start_time:
        end_time += timedelta(days=1)

    current_time = start_time
    shift_earnings = 0.0
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

        rate = BASE_RATE * MULTIPLIERS.get(band, 1.0)
        shift_earnings += rate * hours_per_step
        if band in breakdown:
            breakdown[band] += hours_per_step

        current_time += time_delta

    return shift_earnings, breakdown

# --- File Save/Load Functions ---
def save_roster_file(shifts):
    """Saves the active weekly shift array structure to a specified JSON file."""
    if not shifts:
        print("Error: No shifts to save.")
        return
    
    filename = input("Enter a filename to save your roster (e.g., roster.json): ").strip()
    if not filename.endswith('.json'):
        filename += '.json'
        
    try:
        with open(filename, "w") as f:
            json.dump(shifts, f, indent=4)
        print(f"Roster saved successfully to {filename}!")
    except Exception as e:
        print(f"Error saving file: {str(e)}")

def load_roster_file():
    """Loads shift entries from a JSON file, rebuilding earnings dynamically."""
    filename = input("Enter the filename to load (e.g., roster.json): ").strip()
    if not filename.endswith('.json'):
        filename += '.json'
        
    if not os.path.exists(filename):
        print(f"Error: File '{filename}' not found.")
        return []
        
    try:
        with open(filename, "r") as f:
            loaded_data = json.load(f)
            
        rebuilt_shifts = []
        for item in loaded_data:
            # We recalculate the pay live using the config options active during load
            pay, breakdown = calculate_shift_pay(item["day"], item["start"], item["end"])
            rebuilt_shifts.append({
                "day": item["day"], 
                "start": item["start"], 
                "end": item["end"], 
                "pay": pay, 
                "breakdown": breakdown
            })
        print(f"Roster loaded successfully from {filename}!")
        return rebuilt_shifts
    except Exception as e:
        print(f"Error loading file: {str(e)}")
        return []

def display_summary(shifts):
    """Calculates running totals and prints the final formatted terminal summary."""
    total_weekly_pay = 0.0
    total_shifts = len(shifts)
    weekly_breakdown = {}
    
    for shift in shifts:
        total_weekly_pay += shift["pay"]
        for bracket, hours in shift["breakdown"].items():
            weekly_breakdown[bracket] = weekly_breakdown.get(bracket, 0.0) + hours
            
    laundry_allowance = total_shifts * LAUNDRY_ALLOWANCE_PER_SHIFT
    final_gross_pay = total_weekly_pay + laundry_allowance
    
    print("\n" + "="*45)
    print(" FINAL WEEKLY ESTIMATE ")
    print("="*45)
    print("Hours breakdown across penalty brackets:")
    for bracket, hours in weekly_breakdown.items():
        if hours > 0:
            print(f" * {bracket}: {hours:.2f} hrs")
    
    print("-"*45)
    print(f"Shifts Worked: {total_shifts}")
    print(f"Base Shift Earnings: ${total_weekly_pay:.2f}")
    print(f"Laundry Allowance: ${laundry_allowance:.2f}")
    print("-"*45)
    print(f"TOTAL ESTIMATED PAY: ${final_gross_pay:.2f}")
    print("=============================================")

def main():
    print("==================================================")
    print("      Dynamic Award-Split Pay Calculator         ")
    print(f"      Active Base Profile Rate: ${BASE_RATE:.4f}/hr     ")
    print("==================================================")
    
    weekly_shifts = []
    
    # Check if user wants to pre-load a saved file before manually entering data
    choice = input("Do you want to load an existing roster file? (y/n): ").strip().lower()
    if choice == 'y':
        weekly_shifts = load_roster_file()
        if weekly_shifts:
            display_summary(weekly_shifts)
    
    # Shift Entry Loop
    while True:
        day = input("\nEnter day of shift (e.g., Monday) [or press Enter to finish entering shifts]: ").strip()
        if not day:
            break
        
        valid_days = ["monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday"]
        if day.lower() not in valid_days:
            print("Error: Invalid day name. Please try again.")
            continue
            
        start = input("Enter start time (24h format, e.g., 15:30): ").strip()
        end = input("Enter end time (24h format, e.g., 21:15): ").strip()
        
        shift_pay, shift_breakdown = calculate_shift_pay(day, start, end)
        
        if shift_pay > 0:
            weekly_shifts.append({
                "day": day,
                "start": start,
                "end": end,
                "pay": shift_pay,
                "breakdown": shift_breakdown
            })
            print(f"Success: Added {day} shift (${shift_pay:.2f})")

    # Present final totals
    display_summary(weekly_shifts)
    
    # Save Prompter
    if weekly_shifts:
        save_choice = input("\nDo you want to save this session roster configuration to a file? (y/n): ").strip().lower()
        if save_choice == 'y':
            save_roster_file(weekly_shifts)

if __name__ == "__main__":
    main()

