open DICOM File and display content

SELECT
patient_id.pat_id, 
	person_name.family_name,
	person_name.given_name,
	patient.pat_birthdate,
	patient.pat_sex,
	series.institution,
	study.study_desc,
	series.body_part ,
	"location".storage_path,
	study.storage_ids,
	study.study_date
FROM
	"instance"
	LEFT JOIN  "location" ON "location".instance_fk = "instance".pk
	LEFT JOIN series ON "instance".series_fk = series.pk 

	LEFT JOIN study ON study.pk = series.study_fk
	LEFT JOIN patient ON patient.pk = study.patient_fk
	LEFT JOIN patient_id on patient_id.pk = patient.patient_id_fk
	LEFT JOIN person_name ON person_name.pk = patient.pat_name_fk 
			
	--WHERE person_name.family_name ilike '%JONIVALDO%' OR person_name.given_name ilike '%JONIVALDO%'
	--patient_id.pat_id = '20220512_163213_823512'
	WHERE person_name.family_name ilike '%JUPYRA ALVES DA ROCHA VIDAL%'  
	ORDER BY study.study_date
	LIMIT 20