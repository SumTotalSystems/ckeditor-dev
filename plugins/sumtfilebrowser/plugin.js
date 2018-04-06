CKEDITOR.plugins.add('sumtfilebrowser', {
          icons: 'sumtfilebrowser',
          init: function (editor) {
            //debugger;
            //Plugin logic goes here.
            editor.addCommand("sumtfilebrowser", {
              exec: function (edt) {
				  CKEDITOR.fire('fileUpload');
                //alert(edt.getData());
              }
            });
			// Handle panel destruction.
			// editor.on('destroy', function() {
				// this.destroy();
			// }, this );
			
            editor.ui.addButton('sumtfilebrowser', {

              label: 'My Bold',
              command: 'sumtfilebrowser',
              toolbar: 'insert,0'
            });
          }
        });