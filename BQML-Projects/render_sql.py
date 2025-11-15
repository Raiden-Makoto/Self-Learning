#!/usr/bin/env python3
"""
Script to render SQL files with environment variables.
Replaces ${VAR} placeholders with values from environment variables.
"""

import os
import re
import sys
from pathlib import Path

def load_env_file(env_path='.env'):
    """Load environment variables from .env file."""
    env_vars = {}
    if os.path.exists(env_path):
        with open(env_path, 'r') as f:
            for line in f:
                line = line.strip()
                if line and not line.startswith('#') and '=' in line:
                    key, value = line.split('=', 1)
                    env_vars[key.strip()] = value.strip().strip('"').strip("'")
    return env_vars

def render_sql_file(input_path, output_path=None, env_vars=None):
    """
    Render SQL file by replacing ${VAR} placeholders with environment variables.
    
    Args:
        input_path: Path to input SQL file
        output_path: Path to output SQL file (if None, overwrites input)
        env_vars: Dictionary of environment variables (if None, loads from .env and os.environ)
    """
    if env_vars is None:
        env_vars = {}
        # Load from .env file
        env_vars.update(load_env_file())
        # Override with actual environment variables
        env_vars.update(os.environ)
    
    with open(input_path, 'r') as f:
        content = f.read()
    
    # Replace ${VAR} placeholders
    def replace_var(match):
        var_name = match.group(1)
        return env_vars.get(var_name, match.group(0))  # Return original if not found
    
    rendered = re.sub(r'\$\{(\w+)\}', replace_var, content)
    
    if output_path is None:
        output_path = input_path
    
    with open(output_path, 'w') as f:
        f.write(rendered)
    
    return rendered

def main():
    """Main function to render SQL files."""
    if len(sys.argv) < 2:
        print("Usage: python render_sql.py <sql_file> [output_file]")
        print("Example: python render_sql.py Penguin/TrainPenguinModel.sql")
        sys.exit(1)
    
    input_file = sys.argv[1]
    output_file = sys.argv[2] if len(sys.argv) > 2 else None
    
    if not os.path.exists(input_file):
        print(f"Error: File '{input_file}' not found.")
        sys.exit(1)
    
    try:
        render_sql_file(input_file, output_file)
        output_msg = f"Rendered to '{output_file}'" if output_file else f"Rendered '{input_file}'"
        print(f"✓ {output_msg}")
    except Exception as e:
        print(f"Error: {e}")
        sys.exit(1)

if __name__ == '__main__':
    main()

