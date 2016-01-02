using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;




public class client : MonoBehaviour {
	public static ArrayList cardsDeck = new ArrayList();
	public static ArrayList enemyCardDeck = new ArrayList();


	public void StartClient(){

		Client.connectToServer ();

	}
	
}

public class Client{
	private static TcpClient tcpClient;
	private static NetworkStream networkStream;
	private static StreamReader streamReader;
	private static StreamWriter streamWriter;
	public static void connectToServer(){
		try
		{
			tcpClient = new TcpClient("localhost", 5678);
			
			networkStream = tcpClient.GetStream();
			
			streamReader = new StreamReader(networkStream);
			streamWriter = new StreamWriter(networkStream);
			
			// Передали серверу сообщение

			if (Client.SendMessage(getData.netString).Contains("It's OK"))
				Application.LoadLevel(1);
			else 
				return;

		}
		catch (SocketException i)
		{
			//RecieveDataList.Items.Add("Ошибка: " + i);
		}
	}

	public static String SendMessage(String message){
		streamWriter.WriteLine (message);
		streamWriter.Flush ();
		return streamReader.ReadLine ();

	}
	public static void SendSimpleMessage(String message){
		streamWriter.WriteLine (message);
		streamWriter.Flush ();	
	}

	public static string ReadMessage(){
		return streamReader.ReadLine ();
	}

	public static void SendCardDeck(){
		foreach (String card in client.cardsDeck ) {
			streamWriter.WriteLine (card);
		}
		streamWriter.Flush ();
	}

	public static void loadEnemyCardList(ArrayList enemyArrayList){
		while (enemyArrayList.Count < 15) {
			enemyArrayList.Add (streamReader.ReadLine());
			//Debug.Log(enemyArrayList[enemyArrayList.Count-1]);
		}
	}
}
