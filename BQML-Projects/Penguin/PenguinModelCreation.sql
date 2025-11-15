CREATE OR REPLACE MODEL `${PROJECT_ID}.${PENGUIN_DATASET}.penguins_model`
OPTIONS
  (model_type='linear_reg',
  input_label_cols=['body_mass_g']) AS
SELECT * FROM `bigquery-public-data.ml_datasets.penguins`
WHERE body_mass_G IS NOT NULL;