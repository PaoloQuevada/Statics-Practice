using System;
using UnityEngine;
using UnityEngine.UI;

public class Level8 : MonoBehaviour, IDataPersistence
{
	public GameObject segment1A;

	public GameObject segment1B;

	public GameObject segment2A;

	public GameObject segment2B;

	public GameObject winMenu;

	public ArrowRenderer segment1ALength;

	public ArrowRenderer segment1BLength;

	public ArrowRenderer segment2ALength;

	public ArrowRenderer segment2BLength;

	public Text details;

	public Text wrong;

	public Text horizontal;

	public Text vertical;

	public InputField i;

	public InputField j;

	public InputField k;

	public InputField alpha;

	public InputField beta;

	public InputField gamma;

	private double lengthAnswer;

	private double iAnswer;

	private double jAnswer;

	private double kAnswer;

	private double alphaAnswer;

	private double betaAnswer;

	private double gammaAnswer;

	private Vector3 default1H;

	private Vector3 default1V;

	private Vector3 default2H;

	private Vector3 default2V;

	private int[] point1Segments;

	private int[] point2Segments;

	private int graphState;

	private int playCount;

	private int winCount;

	private int attempts;

	private double avgAttempts;

	private double time;

	private double bestTime;

	public void LoadData(GameData data)
	{
		Debug.Log(data.winCount);
		winCount = data.winCount;
		playCount = data.playCount + 1;
		avgAttempts = data.avgAttempts;
		bestTime = data.bestTimes[0];
	}

	public void SaveData(ref GameData data)
	{
		Debug.Log(winCount);
		data.winCount = winCount;
		data.playCount = playCount;
		if (attempts > 0)
		{
			if (avgAttempts > 0.0)
			{
				data.avgAttempts = ((double)attempts + avgAttempts) / 2.0;
			}
			else
			{
				data.avgAttempts = attempts;
			}
		}
		if (((time < bestTime) | (bestTime == 0.0)) & (time != 0.0))
		{
			data.bestTimes[0] = Math.Round(time, 2);
		}
	}

	private void Awake()
	{
		horizontal.text = "X";
		vertical.text = "Y";
		default1H = segment1A.transform.localPosition;
		default1V = segment1B.transform.localPosition;
		default2H = segment2A.transform.localPosition;
		default2V = segment2B.transform.localPosition;
		string[] array = new string[7];
		Vector3 vector = default1H;
		array[0] = vector.ToString();
		array[1] = " ";
		vector = default1V;
		array[2] = vector.ToString();
		array[3] = " ";
		vector = default2H;
		array[4] = vector.ToString();
		array[5] = " ";
		vector = default2V;
		array[6] = vector.ToString();
		Debug.Log(string.Concat(array));
		point1Segments = new int[3]
		{
			UnityEngine.Random.Range(-10, 11),
			UnityEngine.Random.Range(-10, 11),
			UnityEngine.Random.Range(-10, 11)
		};
		point2Segments = new int[3]
		{
			UnityEngine.Random.Range(-10, 11),
			UnityEngine.Random.Range(-10, 11),
			UnityEngine.Random.Range(-10, 11)
		};
		int[] array2 = new int[3]
		{
			point2Segments[0] - point1Segments[0],
			point2Segments[1] - point1Segments[1],
			point2Segments[2] - point1Segments[2]
		};
		lengthAnswer = Math.Round(Math.Sqrt(Math.Pow(array2[0], 2.0) + Math.Pow(array2[1], 2.0) + Math.Pow(array2[2], 2.0)), 1);
		int num = 10 * UnityEngine.Random.Range(10, 100);
		details.text = "The vector has a magnitude of " + num + "N. \nPoint A is " + point1Segments[0] + " meters away from the origin in the x direction, " + point1Segments[1] + " meters away from the origin in the y direction, " + point1Segments[2] + " meters away from the origin in the z direction. \nPoint B is " + point2Segments[0] + " meters away from the origin in the x direction, " + point2Segments[1] + " meters away from the origin in the y direction, " + point2Segments[2] + " meters away from the origin in the z direction. ";
		Debug.Log(point1Segments[0] + " " + point1Segments[1] + " " + point1Segments[2]);
		Debug.Log(point2Segments[0] + " " + point2Segments[1] + " " + point2Segments[2]);
		Debug.Log(array2[0] + " " + array2[1] + " " + array2[2] + " " + lengthAnswer);
		assignLength(segment1A, segment1ALength, point1Segments[0]);
		assignLength(segment1B, segment1BLength, point1Segments[1]);
		assignLength(segment2A, segment2ALength, point2Segments[0]);
		assignLength(segment2B, segment2BLength, point2Segments[1]);
		segment1A.transform.localPosition = segment1A.transform.localPosition + new Vector3(0f, point1Segments[1], 0f);
		segment1B.transform.localPosition = segment1B.transform.localPosition + new Vector3(point1Segments[0], 0f, 0f);
		segment2A.transform.localPosition = segment2A.transform.localPosition + new Vector3(0f, point2Segments[1], 0f);
		segment2B.transform.localPosition = segment2B.transform.localPosition + new Vector3(point2Segments[0], 0f, 0f);
		iAnswer = Math.Round((double)num * ((double)array2[0] / lengthAnswer));
		jAnswer = Math.Round((double)num * ((double)array2[1] / lengthAnswer));
		kAnswer = Math.Round((double)num * ((double)array2[2] / lengthAnswer));
		alphaAnswer = Math.Round(ConvertToDegrees(Math.Acos((double)array2[0] / lengthAnswer)), 1);
		betaAnswer = Math.Round(ConvertToDegrees(Math.Acos((double)array2[1] / lengthAnswer)), 1);
		gammaAnswer = Math.Round(ConvertToDegrees(Math.Acos((double)array2[2] / lengthAnswer)), 1);
		Debug.Log(iAnswer + ", " + jAnswer + ", " + kAnswer);
		Debug.Log(alphaAnswer + ", " + betaAnswer + ", " + gammaAnswer);
	}

	public void assignLength(GameObject segment, ArrowRenderer segmentLength, int newSegmentLength)
	{
		if (newSegmentLength < 0)
		{
			newSegmentLength *= -1;
			segment.transform.rotation *= Quaternion.Euler(0f, 0f, 180f);
		}
		segmentLength.stemLength = newSegmentLength;
	}

	public void resetSegments()
	{
		segment1A.transform.localPosition = default1H;
		segment1B.transform.localPosition = default1V;
		segment2A.transform.localPosition = default2H;
		segment2B.transform.localPosition = default2V;
		segment1A.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		segment1B.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
		segment2A.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		segment2B.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
	}

	public void rotateGraph()
	{
		if (graphState == 0)
		{
			resetSegments();
			assignLength(segment1A, segment1ALength, point1Segments[0]);
			assignLength(segment1B, segment1BLength, point1Segments[2]);
			assignLength(segment2A, segment2ALength, point2Segments[0]);
			assignLength(segment2B, segment2BLength, point2Segments[2]);
			segment1A.transform.localPosition = segment1A.transform.localPosition + new Vector3(0f, point1Segments[2], 0f);
			segment1B.transform.localPosition = segment1B.transform.localPosition + new Vector3(point1Segments[0], 0f, 0f);
			segment2A.transform.localPosition = segment2A.transform.localPosition + new Vector3(0f, point2Segments[2], 0f);
			segment2B.transform.localPosition = segment2B.transform.localPosition + new Vector3(point2Segments[0], 0f, 0f);
			graphState = 1;
			vertical.text = "Z";
		}
		else if (graphState == 1)
		{
			resetSegments();
			assignLength(segment1A, segment1ALength, point1Segments[1]);
			assignLength(segment1B, segment1BLength, point1Segments[2]);
			assignLength(segment2A, segment2ALength, point2Segments[1]);
			assignLength(segment2B, segment2BLength, point2Segments[2]);
			segment1A.transform.localPosition = segment1A.transform.localPosition + new Vector3(0f, point1Segments[2], 0f);
			segment1B.transform.localPosition = segment1B.transform.localPosition + new Vector3(point1Segments[1], 0f, 0f);
			segment2A.transform.localPosition = segment2A.transform.localPosition + new Vector3(0f, point2Segments[2], 0f);
			segment2B.transform.localPosition = segment2B.transform.localPosition + new Vector3(point2Segments[1], 0f, 0f);
			graphState = 2;
			horizontal.text = "Y";
		}
		else if (graphState == 2)
		{
			resetSegments();
			assignLength(segment1A, segment1ALength, point1Segments[0]);
			assignLength(segment1B, segment1BLength, point1Segments[1]);
			assignLength(segment2A, segment2ALength, point2Segments[0]);
			assignLength(segment2B, segment2BLength, point2Segments[1]);
			segment1A.transform.localPosition = segment1A.transform.localPosition + new Vector3(0f, point1Segments[1], 0f);
			segment1B.transform.localPosition = segment1B.transform.localPosition + new Vector3(point1Segments[0], 0f, 0f);
			segment2A.transform.localPosition = segment2A.transform.localPosition + new Vector3(0f, point2Segments[1], 0f);
			segment2B.transform.localPosition = segment2B.transform.localPosition + new Vector3(point2Segments[0], 0f, 0f);
			graphState = 0;
			horizontal.text = "X";
			vertical.text = "Y";
		}
	}

	public void checkAnswer()
	{
		if (string.IsNullOrWhiteSpace(i.text) | string.IsNullOrWhiteSpace(j.text) | string.IsNullOrWhiteSpace(j.text) | string.IsNullOrWhiteSpace(alpha.text) | string.IsNullOrWhiteSpace(beta.text) | string.IsNullOrWhiteSpace(gamma.text))
		{
			wrong.text = "Please input an answer!";
		}
		else if ((Convert.ToDouble(i.text) == iAnswer) & (Convert.ToDouble(j.text) == jAnswer) & (Convert.ToDouble(k.text) == kAnswer) & (Convert.ToDouble(alpha.text) == alphaAnswer) & (Convert.ToDouble(beta.text) == betaAnswer) & (Convert.ToDouble(gamma.text) == gammaAnswer))
		{
			attempts++;
			winCount++;
			time = Time.timeSinceLevelLoad;
			winMenu.SetActive(value: true);
		}
		else if ((Convert.ToDouble(i.text) != iAnswer) | (Convert.ToDouble(j.text) != jAnswer) | (Convert.ToDouble(k.text) != kAnswer) | (Convert.ToDouble(alpha.text) != alphaAnswer) | (Convert.ToDouble(beta.text) != betaAnswer) | (Convert.ToDouble(gamma.text) != gammaAnswer))
		{
			attempts++;
			Debug.Log(attempts);
			wrong.text = "Try again!";
		}
	}

	public double ConvertToRadians(double angle)
	{
		return Math.PI / 180.0 * angle;
	}

	public double ConvertToDegrees(double angle)
	{
		return 180.0 / Math.PI * angle;
	}
}
