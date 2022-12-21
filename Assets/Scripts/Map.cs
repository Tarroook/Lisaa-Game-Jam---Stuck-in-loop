using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Room currentRoom;
    public static int maxRooms = 8;
    public List<GameObject> allRooms;
    public static List<GameObject> rooms;

    public GameObject[] defaultEnemyList;

    public GameObject player;

    public GameObject[] upgrades;
    public GameObject[] downgrades;

    public delegate void nextRoomAction();
    public static event nextRoomAction onNextRoom;
    public delegate void roomLoadedAction();
    public static event roomLoadedAction onRoomLoaded;

    // Start is called before the first frame update
    void Start()
    {
        rooms = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player");
        generateMap();
        openRoom(1);
    }

    void generateMap()
    {
        for (int i = 0; i < maxRooms; i++)
        {
            int rand = Random.Range(0, allRooms.Count - 1);
            rooms.Add(Instantiate(allRooms[rand], gameObject.transform));
            //allRooms.RemoveAt(rand);
        }

        for(int r = 0; r < rooms.Count; r++)
        {
            rooms[r].GetComponent<Room>().roomNumber = r + 1;
            rooms[r].SetActive(false);
        }
    }

    public void openRoom(int roomNb)
    {
        if (onNextRoom != null)
            onNextRoom();
        if (currentRoom != null)
        {
            currentRoom.gameObject.SetActive(false);
        }
        Debug.Log("Opened room " + roomNb);
        GameObject room = rooms[roomNb - 1];
        currentRoom = room.GetComponent<Room>();
        room.SetActive(true);

        // Delay the call to the updateGraph function until after the currentRoom game object and its children have been fully loaded and activated
        StartCoroutine(DelayUpdateGraph());
    }

    IEnumerator DelayUpdateGraph()
    {
        yield return new WaitForEndOfFrame();
        if (onRoomLoaded != null)
            onRoomLoaded();
    }
}
