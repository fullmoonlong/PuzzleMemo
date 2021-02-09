using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Newtonsoft.Json;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//list를 바로 저장할 수 없어서 만듬
[System.Serializable]
public class Serialization<T>
{
    public Serialization(List<T> _target) => target = _target;
    public List<T> target;
}


[System.Serializable]
public class Animal
{
    //동물의 속성 (이름, 맵, 자기소개, 종, 사이즈, 서식지, 소유 유무)
    public Animal(string _Name, string _Map, string _Intro, string _Species, string _Size, string _Habitat, bool _IsHaving)
    {
        Name = _Name; Map = _Map; Intro = _Intro; Species = _Species; Size = _Size; Habitat = _Habitat; IsHaving = _IsHaving;
    }
    public string Name, Map, Intro, Species, Size, Habitat;
    public bool IsHaving;
}

public class AnimalScript : MonoBehaviour
{
    //스크립트에 저장되어있는 동물 정보 불러오기
    public TextAsset AnimalDatabase;
    //전체 동물 DB, 클릭한 동물 DB, 가지고 있는 동물 DB
    public List<Animal> AllAnimalList, CurAnimalList, MyAnimalList;

    //파일 위치 
    string filePath;

    //처음 맵
    public string curMapType = "정글";

    public GameObject[] AnimalSlot;
    public Image[] AnimalImage, TabImage;
    public Image RealImage;
    public Sprite[] AnimalSprite, RealSprite;
    public Sprite ClickTab, UnClickTab;
    //텍스트 모음
    public Text NameText;
    public Text IntroText;
    public Text SpeciesText;
    public Text SizeText;
    public Text HabitatText;

    //사전 판넬
    public GameObject Dictionary;

    //잠겨있는 사전
    public GameObject Lock;
    void Start()
    {
        //animaldatabase에 있는 텍스트를 allanimal에 대입
        string[] line = AnimalDatabase.text.Substring(0, AnimalDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            AllAnimalList.Add(new Animal(row[0], row[1], row[2], row[3], row[4], row[5], row[6] == "TRUE"));
        }

        //파일 위치 지정
        filePath = Application.persistentDataPath + "/AllAnimal.txt";
        print(filePath);
        LoadFile();
    }

    void Update()
    {

    }

    public void TabMap(string tabName)
    {
        curMapType = tabName;

        CurAnimalList = AllAnimalList.FindAll(x => x.Map == curMapType);

        for (int i = 0; i < AnimalSlot.Length; i++)
        {
            AnimalSlot[i].SetActive(i < CurAnimalList.Count);
            //현재 아이템 리스트 수 안이면 이름써줌, 아니면 이름안써줌
            AnimalSlot[i].transform.GetChild(1).GetComponent<Text>().text = i < CurAnimalList.Count ? CurAnimalList[i].Name : "";

            if (i < CurAnimalList.Count)
            {
                AnimalImage[i].sprite = AnimalSprite[AllAnimalList.FindIndex(x => x.Name == CurAnimalList[i].Name)];
            }

        }

        int tabNum = 0;

        switch (tabName)
        {
            case "정글":
                tabNum = 0;
                break;

            case "사막":
                tabNum = 1;
                break;

            case "남극":
                tabNum = 2;
                break;
        }

        for (int i = 0; i < TabImage.Length; i++)
            TabImage[i].sprite = i == tabNum ? ClickTab : UnClickTab;
    }

    public void PointerEnter(int SlotNum)
    {
        if(CurAnimalList[SlotNum].IsHaving == true)
        {
            Dictionary.SetActive(true);
            switch (CurAnimalList[SlotNum].Name.ToString())
            {
                case "원숭이":
                    RealImage.sprite = RealSprite[0];
                    break;

                case "뱀":
                    RealImage.sprite = RealSprite[1];
                    break;

                case "낙타":
                    RealImage.sprite = RealSprite[2];
                    break;

                case "여우":
                    RealImage.sprite = RealSprite[3];
                    break;

                case "펭귄":
                    RealImage.sprite = RealSprite[4];
                    break;

                case "북극곰":
                    RealImage.sprite = RealSprite[5];
                    break;
            }

            NameText.text = CurAnimalList[SlotNum].Name.ToString();
            IntroText.text = CurAnimalList[SlotNum].Intro.ToString();
            SpeciesText.text = "종 : " + CurAnimalList[SlotNum].Species.ToString();
            SizeText.text = "크기 : " + CurAnimalList[SlotNum].Size.ToString();
            HabitatText.text = "서식지 : " + CurAnimalList[SlotNum].Habitat.ToString();
        }

        else
        {
            Lock.SetActive(true);
        }
    }
    //public void TabAnimalMenu()
    //{
    //    //현재 클릭한 버튼 게임 오브젝트에 넣기
    //    GameObject tempBtn = EventSystem.current.currentSelectedGameObject;

    //    //클릭한 버튼의 자식 text의 이름에 따라 분류
    //    switch (tempBtn.transform.GetChild(1).GetComponent<Text>().text)
    //    {
    //        //만약 원숭이를 가지고 있을 경우 사전이 열림
    //        case "원숭이":
    //            {
    //                if(CurAnimalList[])
    //                break;
    //            }

    //        case "뱀":
    //            {
    //                print("뱀이 클릭되었습니다.");
    //                CurAnimalList = AllAnimalList.FindAll(x => x.Name == "뱀");
    //                CurAnimalList = CurAnimalList.FindAll(x => x.IsHaving);

    //                if (CurAnimalList[0] == null)
    //                    Lock.SetActive(true);
    //                //if (CurAnimalList[0] != null)
    //                //{
    //                //    Dictionary.SetActive(true);
    //                //    NameText.text = CurAnimalList[0].Name.ToString();
    //                //    IntroText.text = CurAnimalList[0].Intro.ToString();
    //                //    SpeciesText.text = "종 : " + CurAnimalList[0].Species.ToString();
    //                //    SizeText.text = "크기 : " + CurAnimalList[0].Size.ToString();
    //                //    HabitatText.text = "서식지 : " + CurAnimalList[0].Habitat.ToString();
    //                //}
    //                break;
    //            }

    //        case "낙타":
    //            {
    //                print("낙타가 클릭되었습니다.");
    //                CurAnimalList = AllAnimalList.FindAll(x => x.Name == "낙타");
    //                CurAnimalList = CurAnimalList.FindAll(x => x.IsHaving);

    //                if (CurAnimalList[0] == null)
    //                    Lock.SetActive(true);
    //                //if (CurAnimalList[0] != null)
    //                //{
    //                //    Dictionary.SetActive(true);
    //                //    NameText.text = CurAnimalList[0].Name.ToString();
    //                //    IntroText.text = CurAnimalList[0].Intro.ToString();
    //                //    SpeciesText.text = "종 : " + CurAnimalList[0].Species.ToString();
    //                //    SizeText.text = "크기 : " + CurAnimalList[0].Size.ToString();
    //                //    HabitatText.text = "서식지 : " + CurAnimalList[0].Habitat.ToString();
    //                //}
    //                break;
    //            }

    //        case "여우":
    //            {
    //                print("여우가 클릭되었습니다.");
    //                CurAnimalList = AllAnimalList.FindAll(x => x.Name == "여우");
    //                CurAnimalList = CurAnimalList.FindAll(x => x.IsHaving);

    //                if (CurAnimalList[0] == null)
    //                    Lock.SetActive(true);
    //                //if (CurAnimalList[0] != null)
    //                //{
    //                //    Dictionary.SetActive(true);
    //                //    NameText.text = CurAnimalList[0].Name.ToString();
    //                //    IntroText.text = CurAnimalList[0].Intro.ToString();
    //                //    SpeciesText.text = "종 : " + CurAnimalList[0].Species.ToString();
    //                //    SizeText.text = "크기 : " + CurAnimalList[0].Size.ToString();
    //                //    HabitatText.text = "서식지 : " + CurAnimalList[0].Habitat.ToString();
    //                //}
    //                break;
    //            }

    //        case "펭귄":
    //            {
    //                print("펭귄이 클릭되었습니다.");
    //                CurAnimalList = AllAnimalList.FindAll(x => x.Name == "펭귄");
    //                CurAnimalList = CurAnimalList.FindAll(x => x.IsHaving);

    //                if (CurAnimalList[0] == null)
    //                    Lock.SetActive(true);
    //                //if (CurAnimalList[0] != null)
    //                //{
    //                //    Dictionary.SetActive(true);
    //                //    NameText.text = CurAnimalList[0].Name.ToString();
    //                //    IntroText.text = CurAnimalList[0].Intro.ToString();
    //                //    SpeciesText.text = "종 : " + CurAnimalList[0].Species.ToString();
    //                //    SizeText.text = "크기 : " + CurAnimalList[0].Size.ToString();
    //                //    HabitatText.text = "서식지 : " + CurAnimalList[0].Habitat.ToString();
    //                //}
    //                break;
    //            }

    //        case "북극곰":
    //            {
    //                print("북극곰이 클릭되었습니다.");
    //                CurAnimalList = AllAnimalList.FindAll(x => x.Name == "북극곰");
    //                CurAnimalList = CurAnimalList.FindAll(x => x.IsHaving);

    //                if (CurAnimalList[0] == null)
    //                    Lock.SetActive(true);
    //                //if (CurAnimalList[0] != null)
    //                //{
    //                //    Dictionary.SetActive(true);
    //                //    NameText.text = CurAnimalList[0].Name.ToString();
    //                //    IntroText.text = CurAnimalList[0].Intro.ToString();
    //                //    SpeciesText.text = "종 : " + CurAnimalList[0].Species.ToString();
    //                //    SizeText.text = "크기 : " + CurAnimalList[0].Size.ToString();
    //                //    HabitatText.text = "서식지 : " + CurAnimalList[0].Habitat.ToString();
    //                //}
    //                break;
    //            }

    //        default:
    //            break;
    //    }
    //}

    //추후에 동물을 얻었을 때
    public void GetAnimal()
    {
        SaveFile();
    }

    void SaveFile()
    {
        //AllAnimalList 직렬화
        string jdata = JsonUtility.ToJson(new Serialization<Animal>(AllAnimalList));
        File.WriteAllText(filePath, jdata);

        TabMap(curMapType);
    }

    void LoadFile()
    {
        if (!File.Exists(filePath))
        {
            ResetItem();
        }
        string jdata = File.ReadAllText(filePath);

        AllAnimalList = JsonUtility.FromJson<Serialization<Animal>>(jdata).target;
        TabMap(curMapType);
    }

    public void ResetItem()
    {
        string jdata = JsonUtility.ToJson(new Serialization<Animal>(AllAnimalList));
        File.WriteAllText(filePath, jdata);
        LoadFile();
    }
}
