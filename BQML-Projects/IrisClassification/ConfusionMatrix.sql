SELECT
  species AS actual_species,
  predicted_species,
  COUNT(*) AS count
FROM ML.PREDICT(MODEL `${PROJECT_ID}.${IRIS_DATASET}.decision_tree`,
  (SELECT * FROM `${PROJECT_ID}.${IRIS_DATASET}.iris`
   WHERE split_group = 'VALIDATION'))
GROUP BY actual_species, predicted_species
ORDER BY actual_species, predicted_species;
