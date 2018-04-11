CKEDITOR.plugins.add('sumtfilebrowser', {
          icons: 'sumtfilebrowser',
          init: function (editor) {
            //Plugin logic goes here.
            editor.addCommand("sumtfilebrowser", {
              exec: function (edt) {
				  CKEDITOR.fire('fileUpload');
              }
            });
			
            editor.ui.addButton('sumtfilebrowser', {
              label: 'File Browser',
              command: 'sumtfilebrowser',
              toolbar: 'insert,0'
            });
          }
        });