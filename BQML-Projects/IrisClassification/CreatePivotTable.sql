SELECT *
FROM (
  SELECT species, predicted_species, COUNT(*) as count
  FROM `${PROJECT_ID}.${IRIS_DATASET}.predictions_validation`
  GROUP BY species, predicted_species
)
PIVOT(
  SUM(count)
  FOR predicted_species IN ('setosa', 'versicolor', 'virginica')
);
