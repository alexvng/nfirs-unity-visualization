# Every building fire in Tempe, Arizona from 2020-2023

*Data from FEMA NFIRS, visualized in Unity using Cesium*

![.gif of Unity project #1](./unity_recording.gif)

![.gif of Unity project #2](./unity_recording2.gif)

This is a Python + Unity project which extracts addresses from the U.S. FEMA [National Fire Incident Reporting System](https://www.usfa.fema.gov/nfirs/access-data/), also known as NFIRS.

Python Jupyter notebooks are used to process the NFIRS raw data, year by year, and convert it to a GeoJSON file.

Google Maps [Geocoding API](https://developers.google.com/maps/documentation/geocoding/overview) is used to convert plaintext addresses into longitude/latitude coordinate pairs.

This file is then imported to Unity and overlaid onto Cesium 3D tiles for an interactive data viewer:

## Step 1: Raw NFIRS data:
![Screenshot of the NFIRS data table](nfirs_raw_image.png)

This data, along with accompanying `incidentaddress` and `codelookup` tables, is cleaned and preprocessed using a [Jupyter notebook](.nfirs_data_notebooks/1_fire_data.ipynb), replacing all alphanumeric codes with plaintext English.

For example, `NO_SPR_OP=2` is changed to "**Number of Sprinklers Operating: 2**", and `ON_SITE_M1=531` to "**On Site Materials #1: Charcoal**".

The address of each building fire is also parsed out from individual columns.

## Step 2:

[Jupyter notebook #2](.nfirs_data_notebooks/2_geocode.ipynb.ipynb) sends each address through the Google Maps [Geocoding API](https://developers.google.com/maps/documentation/geocoding/overview), retrieving the longitude and latitude coordinate pairs.

After combining each year's data using [Jupyter notebook #3](.nfirs_data_notebooks/3_merge_geojson.ipynb), I create a GeoJSON file that contains all the NFIRS data combined with the coordinate pair and other helper columns.

**View the GeoJSON file on a 2D interactive map:** [fires-2020-23.json](https://github.com/alexvng/nfirs-unity-visualization/blob/02e6b9596ff596484bbc59d7a34b50621132f667/Assets/fires-2020-23.json)

