How to add new translations:
1) Follow the guide in Talent at this location {talent}\webui\core\templates\js\CKEditorTranslations.js
2) Once the items are translated, export the translations into a csv file.  Use one file per translation string.  The name of the file should be equal to the name of the js variable in the language file.  
3) Place the CSV file in this same directory as this file.
4) Edit the UpdateTranslations.bat file to add your new file to the list.
5) Run the batch file and verify the translations are updated correctly.



The CKEditorMapping excel file was constructed using information from the following queries:

CKEditor (https://en.wikipedia.org/wiki/List_of_ISO_639-1_codes)
TM codes are in Catcd 1007

select * from CodeAttribute
where catcd = 1007
and (Attr5Val like '%af%'
or Attr5Val like '%ar%'
or Attr5Val like '%az%'
or Attr5Val like '%bg%'
or Attr5Val like '%bn%'
or Attr5Val like '%bs%'
or Attr5Val like '%ca%'
or Attr5Val like '%cs%'
or Attr5Val like '%cy%'
or Attr5Val like '%da%'
or Attr5Val like '%de%'
or Attr5Val like '%de-ch%'
or Attr5Val like '%el%'
or Attr5Val like '%en%'
or Attr5Val like '%en-au%'
or Attr5Val like '%en-ca%'
or Attr5Val like '%en-gb%'
or Attr5Val like '%eo%'
or Attr5Val like '%es%'
or Attr5Val like '%es-mx%'
or Attr5Val like '%et%'
or Attr5Val like '%eu%'
or Attr5Val like '%fa%'
or Attr5Val like '%fi%'
or Attr5Val like '%fo%'
or Attr5Val like '%fr%'
or Attr5Val like '%fr-ca%'
or Attr5Val like '%gl%'
or Attr5Val like '%gu%'
or Attr5Val like '%he%'
or Attr5Val like '%hi%'
or Attr5Val like '%hr%'
or Attr5Val like '%hu%'
or Attr5Val like '%id%'
or Attr5Val like '%is%'
or Attr5Val like '%it%'
or Attr5Val like '%ja%'
or Attr5Val like '%ka%'
or Attr5Val like '%km%'
or Attr5Val like '%ko%'
or Attr5Val like '%ku%'
or Attr5Val like '%lt%'
or Attr5Val like '%lv%'
or Attr5Val like '%mk%'
or Attr5Val like '%mn%'
or Attr5Val like '%ms%'
or Attr5Val like '%nb%'
or Attr5Val like '%nl%'
or Attr5Val like '%no%'
or Attr5Val like '%oc%'
or Attr5Val like '%pl%'
or Attr5Val like '%pt%'
or Attr5Val like '%pt-br%'
or Attr5Val like '%ro%'
or Attr5Val like '%ru%'
or Attr5Val like '%si%'
or Attr5Val like '%sk%'
or Attr5Val like '%sl%'
or Attr5Val like '%sq%'
or Attr5Val like '%sr%'
or Attr5Val like '%sr-latn%'
or Attr5Val like '%sv%'
or Attr5Val like '%th%'
or Attr5Val like '%tr%'
or Attr5Val like '%tt%'
or Attr5Val like '%ug%'
or Attr5Val like '%uk%'
or Attr5Val like '%vi%'
or Attr5Val like '%zh%'
or Attr5Val like '%zh-CN%'
)



select c.code, ca.itmcd,* from
Culture c
inner join TBL_LMS_USERLANG lmLang on lmLang.CultureName = c.Code
inner join CodeAttribute ca on ca.Attr4Val = lmLang.Lang_FK and catcd = 1007
where c.Code in ('af',
'ar',
'az',
'bg',
'bn',
'bs',
'ca',
'cs',
'cy',
'da',
'de-ch',
'de',
'el',
'en-au',
'en-ca',
'en-gb',
'en',
'eo',
'es-mx',
'es',
'et',
'eu',
'fa',
'fi',
'fo',
'fr-ca',
'fr',
'gl',
'gu',
'he',
'hi',
'hr',
'hu',
'id',
'is',
'it',
'ja',
'ka',
'km',
'ko',
'ku',
'lt',
'lv',
'mk',
'mn',
'ms',
'nb',
'nl',
'no',
'oc',
'pl',
'pt-br',
'pt',
'ro',
'ru',
'si',
'sk',
'sl',
'sq',
'sr-latn',
'sr',
'sv',
'th',
'tr',
'tt',
'ug',
'uk',
'vi',
'zh-cn',
'zh'
)

order by c.Code Asc


