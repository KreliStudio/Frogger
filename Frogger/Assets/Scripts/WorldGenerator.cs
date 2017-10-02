using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WorldGenerator : MonoBehaviour
{


    public List<Segment> safezoneSegments;   // Forest, park, grass etc.
    public List<Segment> dangerSegments;   // Roads, trains etc.
    [Range(0.0f, 1.0f)]
    public float safezoneStructuresProbability; // Probability of generate structure, 0- nothing, 1- only structure
    public List<Structure> safezoneStructures;   // Forest, park, grass etc.
    [Range(0.0f, 1.0f)]
    public float dangerStructuresProbability;   // Probability of generate structure, 0- nothing, 1- only structure
    public List<Structure> dangerStructures;   // Roads, trains etc.

    public float dificulty = 0.1f; // dificult easy=0, hard=1
    public Transform player;
    public Transform worldParent;
    
    private long allSegmentsInWorld;  



    void Start()
    {
        StartAreaGenerator();
    }



    void Update()
    {
        // generate world 20 segments front of the player
        if (player != null)
            if (player.position.z >= allSegmentsInWorld - 20)
            {
                WorldGenerate();
            }

    }
    public void StartAreaGenerator()
    {
        // random safezone id and create start safezone are
        int safezoneId = RandomId(safezoneSegments);
        //segments to fill behind player
        int startSafezonePosition = 20;
        allSegmentsInWorld = -startSafezonePosition;
        for (int i = 0; i <= startSafezonePosition; i++)
        {
            safezoneSegments[safezoneId].Create(allSegmentsInWorld++, dificulty);
        }
    }

    public void WorldGenerate()
    {
        if (Random.value <= dangerStructuresProbability)
        {
            // make danger structure
            // random id segment
            int id = RandomId(dangerStructures);
            dangerStructures[id].Create(allSegmentsInWorld);
            allSegmentsInWorld += dangerStructures[id].length;
        }
        else {
            // generate danger part
            GenerateGroupDangerSegments();
        }

        if (Random.value <= safezoneStructuresProbability)
        {
            // make safezone structure
            // random id segment
            int id = RandomId(safezoneStructures);
            safezoneStructures[id].Create(allSegmentsInWorld);
            allSegmentsInWorld += safezoneStructures[id].length;
        }
        else {
            // generate safezone part
            GenerateGroupSafezoneSegments();
        }
    }

    private void GenerateGroupDangerSegments()
    {
        // take maeters from player
        int meters = GameManager.instance.meters;
        //sum of all segments in one group
        int segmentsIsGroup = 0;
        // random group length
        int groupLength = (int)(Random.Range(0, (meters * dificulty)) + Random.Range(1, 4));
        // set group of random segments type
        //Debug.Log("[GS] Wielkość grupy: [" + groupLength + " / " + segmentsIsGroup + "]");
        while (groupLength > segmentsIsGroup)
        {
            // random id segment
            int id = RandomId(dangerSegments);
            // random segment length
            int segmentLength = (int)(Random.Range(1, meters * dificulty * dangerSegments[id].intensity)) + 1;
            segmentsIsGroup += segmentLength;
            // cut segment to fit in group length
            if (groupLength < segmentsIsGroup)
            {
                segmentLength -= (segmentsIsGroup - groupLength);

            }
            // create danger segments
            for (int i = 0; i < segmentLength; i++)
            {
                dangerSegments[id].Create(allSegmentsInWorld++, dificulty);
            }
            //Debug.Log("Wstaw " + dangerSegments[id].prefab.name + ", o długości:  [" + segmentLength +" / " + segmentsIsGroup + "]");
        }
        //Debug.Log("[GS] Wielkość grupy: [" + groupLength + " / " + segmentsIsGroup + "]");
    }

    private void GenerateGroupSafezoneSegments()
    {
        // separate danger segments with safezones
        // random safezone ids and lenght
        int id = RandomId(safezoneSegments);
        int safezonelength = (int)(Random.Range(0, safezoneSegments[id].intensity) / dificulty) + 1;
        // create safezones
        for (int i = 0; i < safezonelength; i++)
        {
            safezoneSegments[id].Create(allSegmentsInWorld++, dificulty);
        }
    }

    public int RandomId(List<Segment> segmentsArray)
    {
        // sum of probability values
        float SumProbability = 0;
        foreach(Segment seg in segmentsArray)
        {
            SumProbability += seg.probability;
        }
        float randomValue = Random.Range(0.0f, SumProbability);

        //Debug.Log("[RId] SumProbability: " + SumProbability +", RandomValue: " +randomValue);
        int i = 0;
        foreach (Segment seg in segmentsArray)
        {
            SumProbability -= seg.probability;
            //Debug.Log("[RId] Check id " +i + " RandomValue > SumProbability: " +randomValue +">=" +SumProbability );
            if (randomValue >= SumProbability)
            {
                return i;
            }
            i++;
        }

        return 0;
    }
    public int RandomId(List<Structure> segmentsArray)
    {
        // sum of probability values
        float SumProbability = 0;
        foreach (Structure seg in segmentsArray)
        {
            SumProbability += seg.probability;
        }
        float randomValue = Random.Range(0.0f, SumProbability);

        //Debug.Log("[RId] SumProbability: " + SumProbability +", RandomValue: " +randomValue);
        int i = 0;
        foreach (Structure seg in segmentsArray)
        {
            SumProbability -= seg.probability;
            //Debug.Log("[RId] Check id " +i + " RandomValue > SumProbability: " +randomValue +">=" +SumProbability );
            if (randomValue >= SumProbability)
            {
                return i;
            }
            i++;
        }

        return 0;
    }


}
