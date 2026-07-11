#!/bin/zsh

# Ensure the script runs from its own directory even if double-clicked
cd "$(dirname "$0")"

# --- Flag Check ---
if [ "$1" == "--gui" ]; then
    echo "Launching GUI version directly..."
    python3 paycalc-gui.py
    exit 0
elif [ "$1" == "--cli" ]; then
    echo "Launching CLI version directly..."
    python3 paycalc-cli.py
    exit 0
elif [ "$1" == "--skip-install" ]; then
    goto_menu=true
fi

# --- Installation Block ---
if [ "$goto_menu" != true ]; then
    echo "Checking environment..."
    # Check if python3 is even installed on the Mac
    if ! command -v python3 &> /dev/null; then
        echo "Error: Python 3 is not installed."
        echo "Please download it from https://python.org"
        exit 1
    else
        echo "Python 3 detected. (Tkinter is pre-bundled on macOS Python builds)."
    fi
fi

# --- Menu Block ---
echo "==================================="
echo "    Pay Calculator Launcher       "
echo "==================================="
echo "Which version would you like to run?"
echo "1) Graphical Interface (GUI)"
echo "2) Command Line Interface (CLI)"
echo "3) Exit"
echo "Hint: Use flags '--gui' or '--cli' to skip this step!"
echo "-----------------------------------"

read -p "Enter your choice [1-3]: " choice

case "$choice" in
  1)
    echo "Launching GUI version..."
    python3 paycalc-gui.py
    ;;
  2)
    echo "Launching CLI version..."
    python3 paycalc-cli.py
    ;;
  3)
    echo "Exiting."
    exit 0
    ;;
  *)
    echo "Invalid option."
    exit 1
    ;;
esac

