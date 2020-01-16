using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GameSaver
{
    public static void save(List<Player> players)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create (Application.persistentDataPath + "/players.bin");

        List<PlayerData> playerData = new List<PlayerData>();
        foreach (var player in players)
        {
            playerData.Add(new PlayerData(player));
        }
        
        bf.Serialize(file, playerData);
        file.Close();
    }
    
    public static void save(List<TrainField> trainFields)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create (Application.persistentDataPath + "/trains.bin");

        List<TrainFieldData> trainFieldData = new List<TrainFieldData>();
        foreach (var trainField in trainFields)
        {
            trainFieldData.Add(new TrainFieldData(trainField));
        }
        
        bf.Serialize(file, trainFieldData);
        file.Close();
    }
    
    public static void save(List<NetworkField> networkFields)
     {
         BinaryFormatter bf = new BinaryFormatter();
         FileStream file = File.Create (Application.persistentDataPath + "/networks.bin");
 
         List<NetworkFieldData> networkFieldsData = new List<NetworkFieldData>();
         foreach (var networkField in networkFields)
         {
             networkFieldsData.Add(new NetworkFieldData(networkField));
         }
         
         bf.Serialize(file, networkFieldsData);
         file.Close();
     }
    
    public static void save(List<Course> courseFields)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create (Application.persistentDataPath + "/courses.bin");

        List<CourseData> coursesData = new List<CourseData>();
        foreach (var course in courseFields)
        {
            coursesData.Add(new CourseData(course));
        }
        
        bf.Serialize(file, coursesData);
        file.Close();
    }
    public static List<PlayerData> loadPlayers()
    {
        BinaryFormatter bf = new BinaryFormatter();
        var path = Application.persistentDataPath + "/players.bin";
        FileStream file = new FileStream(path, FileMode.Open);
        List<PlayerData> dataplayer = bf.Deserialize(file) as List<PlayerData>;
        file.Close();
        return dataplayer;
    }
    
    public static List<TrainFieldData> loadTrainFields()
    {
        BinaryFormatter bf = new BinaryFormatter();
        var path = Application.persistentDataPath + "/trains.bin";
        FileStream file = new FileStream(path, FileMode.Open);
        List<TrainFieldData> data = bf.Deserialize(file) as List<TrainFieldData>;
        file.Close();
        return data;
    }
    
    public static List<NetworkFieldData> loadNetworkFields()
    {
        BinaryFormatter bf = new BinaryFormatter();
        var path = Application.persistentDataPath + "/networks.bin";
        FileStream file = new FileStream(path, FileMode.Open);
        List<NetworkFieldData> data = bf.Deserialize(file) as List<NetworkFieldData>;
        file.Close();
        return data;
    }
    
    public static List<CourseData> loadCourses()
    {
        BinaryFormatter bf = new BinaryFormatter();
        var path = Application.persistentDataPath + "/courses.bin";
        FileStream file = new FileStream(path, FileMode.Open);
        List<CourseData> data = bf.Deserialize(file) as List<CourseData>;
        file.Close();
        return data;
    }
}
