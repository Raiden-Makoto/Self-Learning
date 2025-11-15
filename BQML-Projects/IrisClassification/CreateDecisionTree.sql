CREATE OR REPLACE MODEL `${PROJECT_ID}.${IRIS_DATASET}.decision_tree`
OPTIONS(
  model_type='BOOSTED_TREE_CLASSIFIER',
  input_label_cols = ['species']
)
AS SELECT * FROM `${PROJECT_ID}.${IRIS_DATASET}.iris`
WHERE split_group = 'TRAIN';