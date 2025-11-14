using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private TextAsset waveFile;
    [SerializeField] private EnemyGenarator eg;

    private int waveCount = 0;
    private List<WaveLine> inWaveLines = new List<WaveLine>();

    private void Start()
    {
        LoadWaveFromText();
        GeneratWaveEnemy();
    }

    void LoadWaveFromText()
    {
        inWaveLines.Clear();
        
        if(waveFile == null)//nullCheck
        {
            Debug.LogError("waveFile が設定されていません");
            return;
        }

        string[] lines = waveFile.text.Split("\n");
        bool isFirstLine = true;

        foreach(string raw in lines)
        {
            string line = raw.Trim();
            if (string.IsNullOrEmpty(line)) continue;

            // 1行目はヘッダーなので飛ばす
            if (isFirstLine)
            {
                isFirstLine = false;
                continue;
            }

            //タブで区切る
            string[] cols = line.Split(',');
            if (cols.Length < 4)
            {
                Debug.LogWarning($"列数が足りない行: {line}");
                continue;
            }

            //WaveLineに詰める
            WaveLine w = new WaveLine();
            float.TryParse(cols[0], out w.time);
            w.enemyId = cols[1];
            int.TryParse(cols[2], out w.count);
            int.TryParse(cols[3], out w.lane);

            inWaveLines.Add(w);

        }

        //time 昇順に並べ替え
        inWaveLines.Sort((a, b) => a.time.CompareTo(b.time));

        Debug.Log($"読み込んだWave行数: {inWaveLines.Count}");
        
    }

    void GeneratWaveEnemy()//ここで一行を送って
    {

        eg.CallCoroutine(inWaveLines[waveCount].enemyId,inWaveLines[waveCount].count);
    }

}
