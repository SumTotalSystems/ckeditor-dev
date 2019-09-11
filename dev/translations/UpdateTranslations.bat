SET langPath=F:\Code\CKEditor\ckeditor-dev\plugins\image3\lang
SET binPath=UpdateTranslations\
REM SET binPath=UpdateTranslations\UpdateTranslations\bin\Debug\
SET mappingFile=CKEditorMapping.csv

%binPath%UpdateTranslations.exe %mappingFile% percentage.csv %langPath%
%binPath%UpdateTranslations.exe %mappingFile% pixels.csv %langPath%
%binPath%UpdateTranslations.exe %mappingFile% sizeImageBy.csv %langPath%

pause