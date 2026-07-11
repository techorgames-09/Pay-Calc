#!/bin/bash

# --- Flag Check ---
# Check if the user passed an argument to bypass everything
if [ "$1" == "--gui" ]; then
    echo "Launching GUI version directly..."
    python3 paycalc-gui.py
    exit 0
elif [ "$1" == "--cli" ]; then
    echo "Launching CLI version directly..."
    python3 paycalc-cli.py
    exit 0
elif [ "$1" == "--skip-install" ]; then
    echo "Skipping installation steps as requested."
    # Jumps straight to the menu block down below
    goto_menu=true
fi

# --- Installation Block ---
# Only runs if no bypass flag was matched above
if [ "$goto_menu" != true ]; then
    echo "Installing dependencies:"
    echo "Updating system..."
    sudo apt-get update
    echo "Installing Python3..."
    sudo apt-get install python3-tk -y
fi

# --- Menu Block ---
echo "==================================="
echo "    Pay Calculator Launcher       "
echo "==================================="
echo "Which version would you like to run?"
echo "1) Graphical Interface (GUI)"
echo "2) Command Line Interface (CLI)"
echo "3) Exit"
echo "Hint: Use flags '--gui' or '--cli' to skip this step in future, as well as '--skip-install' to skip the system update!"
echo "-----------------------------------"

# Ask the question and read input
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
    echo "Invalid option. Please run the script again and select 1, 2, or 3."
    exit 1
    ;;
esac

