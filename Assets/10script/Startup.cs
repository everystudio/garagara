using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AssetBundles;

public class Startup : MonoBehaviour {

	public SpriteRenderer m_renderer;

	public void OnClickStartButton()
	{
		// Clear Cache
		//Caching.CleanCache();

		AssetBundleManager.SetSourceAssetBundleURL("https://s3-ap-northeast-1.amazonaws.com/every-studio/app/garagara/AssetBundles/");

		StartCoroutine(DownloadAndCache("Cube", "", 1));
	}

	/// <summary>
	/// Texture2Dとして、アセットを読み込む。
	/// </summary>
	IEnumerator LoadImageFromAssetBundles(string assetBundleName, string assetName)
	{
		Debug.LogError("start");
		AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, assetName, typeof(Sprite));
		if (request == null)
			Debug.LogWarning("There is no asset with name \"" + assetName + "\" in " + assetBundleName + ".");

		yield return StartCoroutine(request);

		Texture2D texture = request.GetAsset<Texture2D>();
		Debug.LogError(texture);


		Sprite sprite2 = request.GetAsset<Sprite>();
		Debug.LogError(sprite2);


		//Sprite sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), Vector2.zero);

		m_renderer.sprite = sprite2;
		Debug.LogError("end");

	}

	public IEnumerator DownloadAndCache(string assetName, string url, int version = 1)
	{
		// キャッシュシステムの準備が完了するのを待ちます
		while (!Caching.ready)
			yield return null;


		yield return StartCoroutine(AssetBundleManager.Initialize());

		yield return StartCoroutine(LoadImageFromAssetBundles("assetbundletexture", "assetbundlesample"));

		/*
		String err = "";
		AssetBundleManager.GetLoadedAssetBundle("assetbundletexture",out err);

		Debug.LogError(err);
		*/

		//yield return StartCoroutine(AssetBundleManager.LoadLevelAsync("scene_forest", "scene_forest", false));
		/*
		// 同じバージョンが存在する場合はアセットバンドルをキャッシュからロードするか、
		//  またはダウンロードしてキャッシュに格納します。
		using (WWW www = WWW.LoadFromCacheOrDownload(url, version))
		{
			yield return www;
			if (www.error != null)
			{
				throw new Exception("WWWダウンロードにエラーがありました:" + www.error);
			}

			AssetBundle bundle = www.assetBundle;
			if (assetName == "")
				Instantiate(bundle.mainAsset);
			else
				Instantiate(bundle.LoadAsset(assetName));
			// メモリ節約のため圧縮されたアセットバンドルのコンテンツをアンロード
			bundle.Unload(false);

		} // memory is freed from the web stream (www.Dispose() gets called implicitly)
		*/
	}


}
