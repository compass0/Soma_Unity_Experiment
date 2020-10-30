using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ButtonControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Upload Image 버튼 클릭 시 서버로 이미지 전송
    public void OnClickUpload(){
        // get image
	string imageName = "user_image2.png"; // 전송할 이미지 파일명(확장자 포함).
	string imagePath = "Assets/Resource/Image/" + imageName;    // 전송할 이미지 파일 패스.
	
	// unity에서 이미지를 import하는 방식
	byte[] byteTexture = System.IO.File.ReadAllBytes(imagePath); 
	Texture2D texture = new Texture2D(0, 0);
	if (byteTexture.Length > 0) {	
		texture.LoadImage(byteTexture);
	}
	Debug.Log(texture);

	// texture byte 자체를 보내면 이미지로 인식이 안되므로 PNG 또는 JPG형식으로 인코딩
	byte[] bytes = null;

	// Encode texture into PNG or JPG
	if (Path.GetExtension(imageName).Equals(".png")){
		bytes = texture.EncodeToPNG();
		Object.Destroy(texture);
	}
	else if(Path.GetExtension(imageName).Equals(".jpg") || Path.GetExtension(imageName).Equals(".jpeg")){
		bytes = texture.EncodeToJPG();
		Object.Destroy(texture);
	}

	Debug.Log(bytes[1]);
	
	// Post 전송 폼
	var type = "0"; // "0" : hair, "0이외" : face
	var data = new List<IMultipartFormSection> {
             new MultipartFormDataSection("type", type),
             new MultipartFormFileSection("userImage", bytes, imageName, "image/png")
        };


	// Django 서버로 전송
	var serverUrl = "http://15.165.15.74:8080/preprocess/"; // 현재 Django의 views.py에서 Post를 구현한 클래스로 연결되는 url
        var handshake = UnityWebRequest.Post(serverUrl, data);
	var fileExtension = (type == "0" ? ".data" : ".png");
	var downloadPath = Path.Combine("Assets/", Path.GetFileNameWithoutExtension(imageName) + fileExtension);
	handshake.downloadHandler = new DownloadHandlerFile(downloadPath);
        var request = handshake.SendWebRequest();
	if (handshake.isNetworkError || handshake.isHttpError)
            Debug.LogError(handshake.error);
        else
            Debug.Log("File successfully downloaded and saved to " + downloadPath);
        Debug.Log("Button Click");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
