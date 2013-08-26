//TODO: 1) attach file-manager to form
//TODO: 2) set upload behaviour (only upload on submit, upload files once all info is filled in, upload files immediately)


var NewFileUploadEvent = "_NEWFILETOUPLOAD_EVENT_";

var uploadBehaviours = {
    OnSubmit: "Submit_Press",
    OnFileSelected: "File_Selected",
    OnFileSelectedFormFilled: "File_Selected_Form_Filled"
};

var fileUploadManagerFactory = {
    defaultFileUploadManagerName: "_##__@@DEFAULTFILEUPLOADMANAGER@@__##",
    fileUploadManagers: [],
    createOrGet: function (name) {
        if (!name) {
            name = this.defaultFileUploadManagerName;
        }

        if (!(name in this.fileUploadManagers)) {
            this.fileUploadManagers[name] = new FileUploadManager(name);
        }
        return this.fileUploadManagers[name];
    }
};


function FileToUpload(file) {
    this.file = file;
};

function FileUploader(fileToUpload) {

    var xhr = jQuery.ajaxSettings.xhr();
    if (xhr.upload) {
        xhr.upload.addEventListener('progress', function (e) {
            // ...
            //TODO: redirect to feeback area
        }, false);
    }

    this.Upload = function () {
        //var reader = new FileReader();
        //reader.readAsDataURL(fileToUpload.file);



        var formData = new FormData();
        formData.append('FileData', fileToUpload.file);

        $.ajax({
            type: 'POST',
            url: 'http://localhost:50572/Api/Files/Upload/', //TODO: grab from elsewhere
            data: formData, // do I have to read file first?
            cache: false,
            contentType: false,
            processData: false,
            xhr: function () {
                return xhr;
            },
            headers: {
                'Content-Disposition': 'form-data',
                'X-File-Name': fileToUpload.file.name,
                'X-File-Size': fileToUpload.file.size,
                'X-File-Type': fileToUpload.file.type
            },
            //beforeSend: function(request) {
            //    //TODO: could put event listener here? - or put this above
            //    request.setRequestHeader("Content-Type", "multipart/form-data");
            //    request.setRequestHeader("X-File-Name", fileToUpload.file.name);
            //    request.setRequestHeader("X-File-Size", fileToUpload.file.size);
            //    request.setRequestHeader("X-File-Type", fileToUpload.file.type);
            //},
            statusCode: {
                404: function () { alert('404'); }
            }
        }).done(function () {
            alert('success');
        }).fail(function (jqXhr, textStatus, errorThrown) {
            alert(textStatus);
            alert(errorThrown);
        });
    };

    //TODO: need something like this http://stackoverflow.com/questions/5157361/jquery-equivalent-to-xmlhttprequests-upload
}


function FileUploadManager(name) {
    this.Name = name;
    var filesToUpload = [];

    //following code checks if method exists so no need for subclass
    //if (typeof(me.onChange) != "undefined") { 
    // safe to use the function

    //how to trigger custom event
    //$.event.trigger({
    //type: "newMessage",
    //    message: "Hello World!",
    //time: new Date()
    //});

    //TODO: lock and use webworkers to upload?? - with anyc loading of file (or at least upload limit - with async uploader)
    //TODO: events : upload started, upload progress, upload completed.
    this.handleFileSelection = function (evt) {
        var newFiles = [];

        var fileUploadManagerName = this.Name;

        $.each(evt.target.files, function (index, value) {
            //TOOD: add properties to file. e.g. user uploading etc
            var fileToUpload = new FileToUpload(value);

            newFiles.push();

            $.event.trigger({
                type: NewFileUploadEvent,
                fileUploadManagerName: fileUploadManagerName,
                fileName: value.name
            });

            //TODO: refactor & get file uploader
            var uploader = new FileUploader(fileToUpload);
            uploader.Upload();
        });

        if (newFiles.length > 0) {
            $.merge(filesToUpload, newFiles);
        }
    };
};


//add an upload class for each behaviour




function MultiAreaFeedbackAdapter(feedbackAreaElement, targetFileManagerName) {
    this.element = $(feedbackAreaElement);
    
    var attribTargetFileManagerName = $(this).attr('target-file-manager-name');

    if (attribTargetFileManagerName)
        targetFileManagerName = attribTargetFileManagerName;

    $(document).on(NewFileUploadEvent, function (evt) {

        if (!targetFileManagerName || evt.fileUploadManagerName == targetFileManagerName) {
            alert('uploading: ' + evt.fileName);
        }
    });
}


(function ($) {
    
    //TODO: set on form, specify file-manager on setFileSelect from form or in form

    $.fn.setFileUploadForm = function() {
        return this.each(function() {
            var fileUploadManagerName = $(this).attr('file-manager-name');
            var fileUploadManager = fileUploadManagerFactory.createOrGet(fileUploadManagerName);

            var fileInputs = $(this).find(':input[type=file]');

            fileInputs.each(function() {
                //register file inpit
            });


            //TODO: detect form submit

        });
    };

    $.fn.setFileSelect = function () {
        return this.each(function () {
            var fileUploadManagerName = $(this).attr('file-manager-name');
            var fileUploadManager = fileUploadManagerFactory.createOrGet(fileUploadManagerName);
            $(this).on('change', fileUploadManager.handleFileSelection);
        });

    };

    $.fn.setFileUploadFeedbackAdapter = function (type, targetFileManagerName) {
        return this.each(function () {
            //TODO: refactor into factory
            if (type == "multi-area") {
                var area = new MultiAreaFeedbackAdapter($(this));
            } else {
                throw "Unknown feedback area type: " + type;
            }
        });
    };

}(jQuery));

