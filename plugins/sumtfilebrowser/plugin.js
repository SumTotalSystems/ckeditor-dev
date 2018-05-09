CKEDITOR.plugins.add('sumtfilebrowser', {
		  lang: 'af,ar,az,bg,bn,bs,ca,cs,da,de,de-ch,el,en,en-au,en-ca,en-gb,eo,es,es-mx,et,eu,fa,fi,fr,fr-ca,gl,gu,he,hi,hr,hu,id,is,it,ja,ka,km,ko,ku,lt,lv,mk,mn,ms,nb,nl,no,pl,pt,pt-br,ro,ru,si,sk,sl,sq,sr,sv,th,tr,uk,vi,zh,zh-cn', // %REMOVE_LINE_CORE%
          icons: 'sumtfilebrowser',
          init: function (editor) {
            //Plugin logic goes here.
            editor.addCommand("sumtfilebrowser", {
              exec: function (edt) {
				  edt.fire('fileUpload');
              }
            });
			
            editor.ui.addButton('sumtfilebrowser', {
              label: editor.lang.sumtfilebrowser.title,
              command: 'sumtfilebrowser',
              toolbar: 'insert,0'
            });
          }
        });