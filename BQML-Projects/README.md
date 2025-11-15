# BQML Projects
**I'm learning BigQuery SQL + BigQueryML + Looker so I (hopefully) can finally get a DS/ML job**

## List of Projects

### NYC Taxi
Simple exploratory dataset analysis practice using Python and BigQuery SQL on the `NYC_Taxi` dataset.
Familiarized myself with the BigQuery Web UI and basic processes like creating datasets, cloning datasets, and simple IAM

### Penguins
Introductory BQML project: simple regression using BigQuery ML and BigQuery SQL.
Predicting the mass of penguins based on data such as species, sex, flipper length, and geographic location.

#### Environment Variables Setup
The SQL files use environment variables to keep sensitive project information private. Both **Penguins** and **IrisClassification** projects use this setup.

1. Create a `.env` file in the project root with your BigQuery credentials:
   ```
   PROJECT_ID=your-project-id
   PENGUIN_DATASET=SimpleRegressionDataset
   IRIS_DATASET=IrisDataset
   ```

2. Render SQL files before executing:
   - **Using Python**: `python render_sql.py Penguin/TrainPenguinModel.sql`
   - **Using Bash**: `./render_sql.sh Penguin/TrainPenguinModel.sql`
   
   The script will replace `${PROJECT_ID}`, `${PENGUIN_DATASET}`, and `${IRIS_DATASET}` placeholders with values from your `.env` file.

3. The `.env` file is automatically ignored by git to keep your credentials private.

### Iris Classification
Decision Tree Classifier on the BigQuery Iris dataset.
Practiced BigQuery ML and creating reports in Looker Studio.

### NCAA Bracketology
Google Skills Course project analyzing NCAA basketball data and building predictive models for March Madness tournament outcomes.

**Exploratory Data Analysis** (`EDA/NCAA_EDA.ipynb`):
- Analyzed play-by-play data from the `bigquery-public-data.ncaa_basketball` dataset
- Explored event types (two-pointers, free throws, three-pointers) and game statistics
- Identified games with the most three-point shots, highest scoring games, and biggest point differentials
- Queried historical tournament games across 33+ seasons of data

**Machine Learning Model** (`ML/NCAA_Classification.ipynb`):
- Built a **logistic regression classification model** using BigQuery ML to predict game outcomes (win/loss)
- Features include: season, team seed (ranking), school name, opponent seed, and opponent school
- Trained on historical tournament games (seasons ≤ 2017) from the NCAA basketball dataset
- Evaluated model performance using accuracy, precision, recall, F1 score, ROC AUC, and log loss
- Achieved ~70% accuracy on 2018 tournament predictions (38 incorrect predictions out of 134 total)
- Analyzed model weights to understand feature importance (seed rankings and school names)
- Performed diagnostic analysis on high-confidence incorrect predictions to identify model weaknesses

## Subway GIS
Learning Google BigQuery GIS by querying the `new_york_subway` public dataset.
- Profiled the `stations` table schema, surfaced unique line names, and summarized station counts by borough.
- Built proximity searches with `ST_DISTANCE` + `ST_GEOGPOINT` to rank stops closest to 23 Cornelia Street.
- Used `ST_WITHIN` to join station points to borough polygons and explore neighborhood coverage.
- Clustered dense corridors with `ST_CLUSTERDBSCAN` and outlined them via `ST_CONVEXHULL` for quick mapping prototypes.