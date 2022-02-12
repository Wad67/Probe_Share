using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ConsoleRedirect : MonoBehaviour
{
    // Adjust via the Inspector
    public int maxLines = 8;
    public GameObject textMesh;
    private Queue<string> queue = new Queue<string>();
    private string currentText = "";
    // Start is called before the first frame update
    void OnEnable()
    {
        Application.logMessageReceivedThreaded += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceivedThreaded -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Delete oldest message
        if (queue.Count >= maxLines) queue.Dequeue();

        queue.Enqueue(logString);

        var builder = new StringBuilder();
        foreach (string st in queue)
        {
            builder.Append(st).Append("\n");
        }

        currentText = builder.ToString();
    }

    void OnGUI()
    {
       textMesh.GetComponent<TextMeshProUGUI>().text = currentText;
    }
}
