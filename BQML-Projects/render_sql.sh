#!/bin/bash
# Simple shell script to render SQL files with environment variables
# Usage: ./render_sql.sh <sql_file> [output_file]

if [ $# -lt 1 ]; then
    echo "Usage: ./render_sql.sh <sql_file> [output_file]"
    echo "Example: ./render_sql.sh Penguin/TrainPenguinModel.sql"
    exit 1
fi

INPUT_FILE="$1"
OUTPUT_FILE="${2:-$INPUT_FILE}"

if [ ! -f "$INPUT_FILE" ]; then
    echo "Error: File '$INPUT_FILE' not found."
    exit 1
fi

# Load .env file if it exists
if [ -f .env ]; then
    export $(cat .env | grep -v '^#' | xargs)
fi

# Replace ${VAR} placeholders with environment variables
envsubst < "$INPUT_FILE" > "$OUTPUT_FILE"

echo "✓ Rendered '$INPUT_FILE' to '$OUTPUT_FILE'"

