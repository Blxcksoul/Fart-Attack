using UnityEngine;
using System;
using System.Collections;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;
using Random = UnityEngine.Random;

public class ConnectDB : MonoBehaviour
{
    public static MySqlConnection dbConnection;
    public static MySqlDataReader reader;
    static string host = "fartattack.cokzawvi3jza.ap-southeast-1.rds.amazonaws.com";
    static string port = "3306";
    static string id = "admin";
    static string pwd = "$t3pping$t0ne";
    static string database = "fa_levels";

    public string savefile;

    public static ConnectDB instance { get; private set; }

    public static bool StartConnection()
    {
        try
        {
            dbConnection = new MySqlConnection("Server= " + host + "; Port =" + port + "; uid =" + id + "; Pwd =" + pwd + "; Database =" + database + "; charset=utf8;");
            dbConnection.Open();
            Debug.Log("Connection started");
            string alter = "ALTER TABLE LevelData CONVERT TO CHARACTER SET utf8";
            Debug.Log(alter);
            var cmd = new MySqlCommand(alter, dbConnection);
            cmd.ExecuteNonQuery();

            return true;
        }
        catch (Exception e)
        {
            throw new Exception("error" + e.Message.ToString());
        }
    }

    public static void InsertData(int gameobject, float posX, float posZ, float euler)
    {
        string sql = "INSERT INTO LevelData (_id, object, posX, posZ, euler) VALUES(" + "LAST_INSERT_ID(), " + gameobject + ", " + posX + ", " + posZ + ", " + euler + ")";
        var cmd = new MySqlCommand(sql, dbConnection); 
        cmd.ExecuteNonQuery();
    }

    public static void Insert()
    {
        string levelid = "INSERT INTO Levels (id) VALUES(NULL)";
        var cmd = new MySqlCommand(levelid, dbConnection);
        cmd.ExecuteNonQuery();
    }

    public static void Delete()
    {
        string reset = " DELETE FROM Levels; ALTER TABLE Levels AUTO_INCREMENT = 0;";
        var cmd = new MySqlCommand(reset, dbConnection);
        cmd.ExecuteNonQuery();
    }

    public static string Pull()
    {
        string savefile = string.Empty;
        string data;
        string count = "SELECT COUNT(*) FROM Levels";
        var cmd = new MySqlCommand(count, dbConnection);
        int x = Convert.ToInt32(cmd.ExecuteScalar());
        Debug.Log("Current Data count: " + x);
        if (x == 0)
        {
            Debug.Log("no data found");
            return null;
        }
        else
        {
            int rand = Random.Range(1, x);
            string sql = "SELECT * FROM LevelData WHERE LevelData._id = '" + rand + "'";
            Debug.Log(sql);
            var newcmd = new MySqlCommand(sql, dbConnection);
            reader = newcmd.ExecuteReader();
            while (reader.Read())
            {
                savefile += (reader.GetString(1) + "|" + reader.GetString(2) + "|" + reader.GetString(3) + "|" + reader.GetString(4) + "|");
            }
            reader.Close();
            Debug.Log(savefile);
            data = savefile;
            return data;
        }        
    }
    
    public void CloseConnection()
    {

        if (dbConnection != null)
        {
            dbConnection.Close();
            dbConnection.Dispose();
            dbConnection = null;
        }
    }

    
}
