using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public Vector4[,] map;  //holds pathways in dungeon
    //each node is a vector4(right=2,bottom=4,left=8,top=1) 
    public int[,] roomTypes;    //what is in each of the rooms
    //0 = nothing, 1 = enemies, 2 = shop, 3 = treasure, 4 = events
    public int[,] visibility;
    //0 = cant see, 1 = can see, 2 = scouted
    public GameObject[,] encounters;
    public GameObject encounterPrefab;

    public Tilemap roomTilemap;
    public Tilemap overlayTilemap;
    public Tile[] roomTiles;
    public Tile[] overLayTiles;

    public int mapWidth, mapHeight;

    public void CreateMap(int seed)
    {
    
        Debug.Log("Creating Map");
        map = new Vector4[mapWidth, mapHeight];
        roomTypes = new int[mapWidth, mapHeight];
        visibility = new int[mapWidth, mapHeight];
        encounters = new GameObject[mapWidth, mapHeight];

        //create pathways

        map[0, mapHeight - 1] = new Vector4(2, 4, 0, 0);
        map[mapWidth - 1, 0] = new Vector4(0, 0, 8, 1);
        List<Vector2> holdPositions = new List<Vector2>();  //starts with list of all spots
        //initialize holdpositions
        for (int j = 0; j < mapWidth; j++)
        {
            for (int k = 0; k < mapHeight; k++)
            {
                if (VectorToInt(map[j, k]) == 0)
                {
                    holdPositions.Add(new Vector2(j, k));
                }
            }
        }
        //holdPositions.RemoveAt(4);  //dont change start room
        //holdPositions.RemoveAt(19); //dont change end room
        //add rooms to map
        int length = holdPositions.Count;
        for (int i = 0; i < length; i++)
        {
            //remove and store random room
            int index = Random.Range(0, holdPositions.Count);
            Vector2 roomPos = holdPositions[index];
            holdPositions.RemoveAt(index);
            //check surrounding rooms
            int w = 0;
            int x = 0;
            int y = 0;
            int z = 0;
            //map border
            if (roomPos.x == 0)
                z = 2;
            if (roomPos.x == mapWidth - 1)
                x = 2;
            if (roomPos.y == 0)
                y = 2;
            if (roomPos.y == mapHeight - 1)
                w = 2;
            //already placed rooms that dont connect
            if (w != 2 && VectorToInt(map[(int)roomPos.x, (int)roomPos.y + 1]) != 0)
                w = 3;
            if (x != 2 && VectorToInt(map[(int)roomPos.x + 1, (int)roomPos.y]) != 0)
                x = 3;
            if (y != 2 && VectorToInt(map[(int)roomPos.x, (int)roomPos.y - 1]) != 0)
                y = 3;
            if (z != 2 && VectorToInt(map[(int)roomPos.x - 1, (int)roomPos.y]) != 0)
                z = 3;
            //check for doorways
            if (w != 2 && map[(int)roomPos.x, (int)roomPos.y + 1].y == 4)
                w = 1;
            if (x != 2 && map[(int)roomPos.x + 1, (int)roomPos.y].z == 8)
                x = 1;
            if (y != 2 && map[(int)roomPos.x, (int)roomPos.y - 1].w == 1)
                y = 1;
            if (z != 2 && map[(int)roomPos.x - 1, (int)roomPos.y].x == 2)
                z = 1;
            //convert already placed rooms back to 2
            if (w == 3)
                w = 2;
            if (x == 3)
                x = 2;
            if (y == 3)
                y = 2;
            if (z == 3)
                z = 2;
            //Debug.Log("Looking for:" + w + x + y + z);
            Vector4 room = FindRandomRoom(w, x, y, z);
            //make sure all rooms have doors
            if (VectorToInt(room) == 0)
            {
                if (roomPos.y > 0)
                {
                    //add doorway on bottom
                    room.y = 4;
                    //connect adjacent room
                    map[(int)roomPos.x, (int)roomPos.y - 1].w = 1;
                }
                else
                {
                    //add doorway on top
                    room.w = 1;
                    map[(int)roomPos.x, (int)roomPos.y + 1].y = 4;
                }
            }
            //place room
            map[(int)roomPos.x, (int)roomPos.y] = room;
            //Debug.Log("Set Map[" + roomPos.x + "," + roomPos.y + "] to (" + room.w + "," + room.x + "," + room.y + "," + room.z + ")");
        }
        //make sure all rooms connect
        int[,] connectedRooms = new int[mapWidth, mapHeight];    //1 = connected to start
        List<Vector2> currentPath = new List<Vector2> { new Vector2(0, mapHeight - 1) };
        Pathfind(connectedRooms, currentPath);
        while (AddUpInts(connectedRooms) != mapWidth * mapHeight - 1)
        {
            Vector2 nextRoomPos = new Vector2();
            bool foundRoom = false;
            int skipAmount = 0;
            while (!foundRoom && skipAmount < 30)
            {
                nextRoomPos = FindNextIsolatedRoom(connectedRooms, skipAmount);
                //connect next path to start path
                foundRoom = true;
                if (nextRoomPos.y < mapHeight - 1 && connectedRooms[(int)nextRoomPos.x, (int)nextRoomPos.y + 1] == 1)
                {
                    //add doorway on top
                    map[(int)nextRoomPos.x, (int)nextRoomPos.y].w = 1;
                    //connect adjacent room
                    map[(int)nextRoomPos.x, (int)nextRoomPos.y + 1].y = 4;
                }
                else if (nextRoomPos.y > 0 && connectedRooms[(int)nextRoomPos.x, (int)nextRoomPos.y - 1] == 1)
                {
                    //add doorway on bottom
                    map[(int)nextRoomPos.x, (int)nextRoomPos.y].y = 4;
                    //connect adjacent room
                    map[(int)nextRoomPos.x, (int)nextRoomPos.y - 1].w = 1;
                }
                else if (nextRoomPos.x < mapWidth - 1 && connectedRooms[(int)nextRoomPos.x + 1, (int)nextRoomPos.y] == 1)
                {
                    //add doorway on right
                    map[(int)nextRoomPos.x, (int)nextRoomPos.y].x = 2;
                    //connect adjacent room
                    map[(int)nextRoomPos.x + 1, (int)nextRoomPos.y].z = 8;
                }
                else if (nextRoomPos.x > 0 && connectedRooms[(int)nextRoomPos.x - 1, (int)nextRoomPos.y] == 1)
                {
                    //Debug.Log("connected path by left (" + nextRoomPos.x + "," + nextRoomPos.y + ")");
                    //add doorway on left
                    map[(int)nextRoomPos.x, (int)nextRoomPos.y].z = 8;
                    //connect adjacent room
                    map[(int)nextRoomPos.x - 1, (int)nextRoomPos.y].x = 2;
                }
                else
                {
                    //search for new isolated square
                    Debug.Log("not connected path (" + nextRoomPos.x + "," + nextRoomPos.y + ")");
                    foundRoom = false;
                    skipAmount++;
                }
            }
            currentPath.Add(nextRoomPos);
            Pathfind(connectedRooms, currentPath);
        }
        //Debug.Log("connected rooms: " + AddUpInts(connectedRooms));


        //place room types

        //enemies
        int enemyCount = 0;
        while (enemyCount < ((mapWidth * mapHeight) / 3) + 2)
        {
            int xTile = Random.Range(0, mapWidth - 2);
            for (int yTile = 0; yTile < mapHeight; yTile++)
            {
                if (enemyCount < ((mapWidth * mapHeight) / 3) + 2)
                {
                    enemyCount++;
                    roomTypes[xTile, yTile] = 1;
                    encounters[xTile, yTile] = GenerateEncounter(1, EncounterType.ENEMY);
                }
                xTile += Random.Range(2, 5);
                xTile %= mapWidth;
                if (Random.Range(1, 5) == 1)
                    yTile++;
            }
        }
        //treasure
        int treasuresPlaced = 0;
        for (int j = 0; j < mapWidth; j++)
        {
            for (int k = 0; k < mapHeight; k++)
            {
                //dont place treasure too close to spawn
                if (j + (mapHeight - 1 - k) > 2)
                {
                    //look for dead ends
                    if (VectorToInt(map[j, k]) == 1 || VectorToInt(map[j, k]) == 2
                        || VectorToInt(map[j, k]) == 4 || VectorToInt(map[j, k]) == 8)
                    {
                        //dont place more than 2 treasures
                        if (treasuresPlaced < 2)
                        {
                            roomTypes[j, k] = 3;
                            encounters[j, k] = GenerateEncounter(1, EncounterType.TREASURE);
                            treasuresPlaced++;
                            //place monster guarding it
                            if (map[j, k].w == 1)
                            {
                                roomTypes[j, k + 1] = 1;
                            }
                            if (map[j, k].x == 2)
                            {
                                roomTypes[j + 1, k] = 1;
                            }
                            if (map[j, k].y == 4)
                            {
                                roomTypes[j, k - 1] = 1;
                            }
                            if (map[j, k].z == 8)
                            {
                                roomTypes[j - 1, k] = 1;
                            }
                        }
                    }
                }
            }
        }
        for (int i = treasuresPlaced; i < 2; i++)
        {
            int xPos = Random.Range(0, mapWidth);
            int yPos = Random.Range(0, mapHeight);
            //dont put treasure in start/end rooms
            if ((xPos == 0 && yPos == mapHeight - 1) || (xPos == mapWidth - 1 && yPos == 0))
            {
                i--;
            }
            else
            {
                roomTypes[xPos, yPos] = 3;
                encounters[xPos, yPos] = GenerateEncounter(1, EncounterType.TREASURE);
            }
        }
        //shop
        bool shopPlaced = false;
        int limit = 0;
        while (!shopPlaced)
        {
            if (Random.Range(1, 101) < 40)
            {
                //spawn shop in bottom left corner
                int xPos = Random.Range(0, 2);
                int yPos = Random.Range(0, 2);
                //dont overwrite treasure. can overwrite enemies after 50 tries
                if (roomTypes[xPos, yPos] == 0 || (limit > 50 && roomTypes[xPos, yPos] < 2))
                {
                    roomTypes[xPos, yPos] = 2;
                    encounters[xPos, yPos] = GenerateEncounter(1, EncounterType.SHOP);
                    shopPlaced = true;
                }
            }
            else if(Random.Range(1, 101) < 75)
            {
                //spawn shop in top right corner
                int xPos = Random.Range(mapWidth - 2, mapWidth);
                int yPos = Random.Range(mapWidth - 2, mapWidth);
                //dont overwrite treasure.  can overwrite enemies after 50 tries
                if (roomTypes[xPos, yPos] == 0 || (limit > 50 && roomTypes[xPos, yPos] < 2))
                {
                    roomTypes[xPos, yPos] = 2;
                    encounters[xPos, yPos] = GenerateEncounter(1, EncounterType.SHOP);
                    shopPlaced = true;
                }
            }
            else
            {
                //spawn shop in center of board
                //dont overwrite treasure.  can overwrite enemies after 50 tries
                if (roomTypes[mapWidth / 2, mapHeight / 2] == 0 || (limit > 50 && roomTypes[mapWidth / 2, mapHeight / 2] < 2))
                {
                    roomTypes[mapWidth / 2, mapHeight / 2] = 2;
                    shopPlaced = true;
                }
            }
            limit++;
        }
        //events
        int numOfEvents = Random.Range(5, 9);
        for(int i = 0; i < numOfEvents; i++)
        {
            int xPos = Random.Range(0, mapWidth);
            int yPos = Random.Range(0, mapHeight);
            //dont overwrite enemies/shops/treasure
            if (roomTypes[xPos, yPos] == 0)
            {
                roomTypes[xPos, yPos] = 4;
                encounters[xPos, yPos] = GenerateEncounter(1, EncounterType.EVENT);
            }
            else
            {
                i--;
            }

        }
        //start and end rooms
        roomTypes[0, mapHeight - 1] = 0;
        roomTypes[mapWidth - 1, 0] = 0;

        visibility[0, mapHeight - 1] = 1;
        DrawMap();
    }

    //places tiles onto tilemap
    public void DrawMap()
    {
        //room tiles
        for (int j = 0; j < mapWidth; j++)
        {
            for (int k = 0; k < mapHeight; k++)
            {
                //if(visibility[j,k] == 1)
                    roomTilemap.SetTile(new Vector3Int(j, k, 0), roomTiles[VectorToInt(map[j, k])]);
            }
        }
        //room overlays
        for (int j = 0; j < mapWidth; j++)
        {
            for (int k = 0; k < mapHeight; k++)
            {
                //if (roomTypes[j, k] > 0 && visibility[j,k] == 1)
                    overlayTilemap.SetTile(new Vector3Int(j, k, 0), overLayTiles[roomTypes[j, k]]);
            }
        }
    }


    //takes an int and converts it to the corresponding vector4
    private Vector4 IntToVector(int i)
    {
        Vector4 vector = new Vector4(0, 0, 0, 0);
        //convert to binary
        int[] a = new int[4];
        for (int n = 0; i > 0; n++)
        {
            a[n] = i % 2;
            i /=  2;
        }
        //convert binary to vector
        vector.w = a[0] * 1;
        vector.x = a[1] * 2;
        vector.y = a[2] * 4;
        vector.z = a[3] * 8;
        //return
        return vector;
    }

    //adds up each of the numbers in the vector
    private int VectorToInt(Vector4 v)
    {
        int sum = 0;
        sum += (int)v.w;
        sum += (int)v.x;
        sum += (int)v.y;
        sum += (int)v.z;
        return sum;
    }

    //create a room with random doors  params: 0 = doesnt matter, 1 = must be there, 2 = cant be there
    private Vector4 FindRandomRoom(int w, int x, int y, int z)
    {
        int count = 0;
        Vector4 v = new Vector4();
        bool up = false, down = false, left = false, right = false ;
        while (!(up && down && left && right) && count < 100)
        {
            up = false;
            down = false;
            left = false;
            right = false;

            int num = Random.Range(1, 16);
            v = IntToVector(num);
            if (w == 0)
                up = true;
            if (w == 1 && v.w == 1)
                up = true;
            if (w == 2 && v.w == 0)
                up = true;
            
            if (x == 0)
                right = true;
            if (x == 1 && v.x == 2)
                right = true;
            if (x == 2 && v.x == 0)
                right = true;
            
            if (y == 0)
                down = true;
            if (y == 1 && v.y == 4)
                down = true;
            if (y == 2 && v.y == 0)
                down = true;
            
            if (z == 0)
                left = true;
            if (z == 1 && v.z == 8)
                left = true;
            if (z == 2 && v.z == 0)
                left = true;
            count++;
        }
        //failsafe
        if (count == 100)
        {
            v = new Vector4(0, 0, 0, 0);
            if (w == 2)
                v.w = 0;
            if (x == 2)
                v.x = 0;
            if (y == 2)
                v.y = 0;
            if (z == 2)
                v.z = 0;
            if (w == 1)
                v.w = 1;
            if (x == 1)
                v.x = 2;
            if (y == 1)
                v.y = 4;
            if (z == 1)
                v.z = 8;
        }
        //Debug.Log("Using vector" + ": (" + v.w + "," + v.x + "," + v.y + "," + v.z + ") - count: " + count );
        return v;
    }

    //adds up all ints in an int[,]
    private int AddUpInts(int[,] array)
    {
        int sum = 0;
        foreach(int i in array)
        {
            sum += i;
        }
        return sum;
    }

    //recursive function that starts at path and marks all connected rooms in connectedRooms as 1
    private void Pathfind(int[,] connectedRooms, List<Vector2> path)
    {
        if (path.Count == 0)
            return;
        int xPos = (int)path[path.Count - 1].x;
        int yPos = (int)path[path.Count - 1].y;
        //ignores end room, since players exit dungeon when they enter end room
        if (xPos == mapWidth - 1 && yPos == 0)
        {
            path.RemoveAt(path.Count - 1);
            return;
        }
        connectedRooms[xPos, yPos] = 1;
        //Debug.Log("Room connected: (" + xPos + ", " + yPos + ")");
        //look for next room
        if (map[xPos, yPos].w == 1 && connectedRooms[xPos, yPos + 1] == 0) //current room has a top door
        {
            path.Add(new Vector2(xPos,yPos + 1));
            Pathfind(connectedRooms, path);
        }
        if(map[xPos, yPos].x == 2 && connectedRooms[xPos + 1, yPos] == 0)
        {
            path.Add(new Vector2(xPos + 1, yPos));
            Pathfind(connectedRooms, path);
        }
        if (map[xPos, yPos].y == 4 && connectedRooms[xPos, yPos - 1] == 0)
        {
            path.Add(new Vector2(xPos, yPos - 1));
            Pathfind(connectedRooms, path);
        }
        if (map[xPos, yPos].z == 8 && connectedRooms[xPos - 1, yPos] == 0)
        {
            path.Add(new Vector2(xPos - 1, yPos));
            Pathfind(connectedRooms, path);
        }
        if(path.Count >= 1)
            path.RemoveAt(path.Count - 1);
        if(path.Count == 1)
            path.RemoveAt(0);
    }

    //returns a room that is not connected to the start
    private Vector2 FindNextIsolatedRoom(int[,] rooms, int skipAmount)
    {
        int skipCount = 0;
        for (int j = 0; j < mapWidth; j++)
        {
            for (int k = mapHeight - 1; k >= 0; k--)
            {
                if (rooms[j, k] == 0 && !(j == mapWidth - 1 && k == 0))
                {
                    if (skipCount >= skipAmount)
                    {
                        //Debug.Log("next room: (" + j + "," + k + ")");
                        return new Vector2(j, k);
                    }
                    else
                    {
                        skipCount++;
                    }
                }
            }
        }
        return new Vector2(0,0);
    }

    private GameObject GenerateEncounter(float difficulty, EncounterType type)
    {
        GameObject newEncounter = Instantiate(encounterPrefab, this.transform);
        newEncounter.GetComponent<Encounter>().type = type;
        newEncounter.GetComponent<Encounter>().GenerateEncounter(difficulty);
        newEncounter.SetActive(false);
        return newEncounter;
    }
}
