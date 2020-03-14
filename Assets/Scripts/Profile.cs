using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Profile : MonoBehaviour
{
	#region Singlton:Profile

	public static Profile Instance;

	void Awake ()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy (gameObject);
	}

	#endregion

	public class Avatar
	{
		public Sprite Image;
	}

	public List<Avatar> AvatarsList;

	[SerializeField] GameObject AvatarUITemplate;
	[SerializeField] Transform AvatarsScrollView;

	GameObject g;
	int newSelectedIndex, previousSelectedIndex;

	[SerializeField] Color ActiveAvatarColor;
	[SerializeField] Color DefaultAvatarColor;

	[SerializeField] Image CurrentAvatar;

	void Start ()
	{
		GetAvailableAvatars ();
		newSelectedIndex = previousSelectedIndex = 0;
	}

	void GetAvailableAvatars ()
	{
		for (int i = 0; i < Shop.Instance.ShopItemsList.Count; i++) {
			if (Shop.Instance.ShopItemsList [i].IsPurchased) {
				//add all purchased avatars to AvatarsList
				AddAvatar (Shop.Instance.ShopItemsList [i].Image);
			}
		}

		SelectAvatar (newSelectedIndex);
	}

	public void AddAvatar (Sprite img)
	{
		if (AvatarsList == null)
			AvatarsList = new List<Avatar> ();
		
		Avatar av = new Avatar (){ Image = img };
		//add av to AvatarsList
		AvatarsList.Add (av);

		//add avatar in the UI scroll view
		g = Instantiate (AvatarUITemplate, AvatarsScrollView);
		g.transform.GetChild (0).GetComponent <Image> ().sprite = av.Image;

		//add click event
		g.transform.GetComponent <Button> ().AddEventListener (AvatarsList.Count - 1, OnAvatarClick);
	}

	void OnAvatarClick (int AvatarIndex)
	{
		SelectAvatar (AvatarIndex);
	}

	void SelectAvatar (int AvatarIndex)
	{
		previousSelectedIndex = newSelectedIndex;
		newSelectedIndex = AvatarIndex;
		AvatarsScrollView.GetChild (previousSelectedIndex).GetComponent <Image> ().color = DefaultAvatarColor;
		AvatarsScrollView.GetChild (newSelectedIndex).GetComponent <Image> ().color = ActiveAvatarColor;

		//Change Avatar
		CurrentAvatar.sprite = AvatarsList [newSelectedIndex].Image;
	}
}
