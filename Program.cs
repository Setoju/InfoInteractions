using System;

class InfoInteractions
{
    public static void Main(string[] args)
    {
        FirstIssueInfo firstNode = new FirstIssueInfo("1" ,10, 0.5, 20);
        FirstIssueInfo secondNode = new FirstIssueInfo("2" ,5, 0.8, 35);
        FirstIssueInfo thirdNode = new FirstIssueInfo("3" ,8, 0.4, 18);
        FirstIssueInfo fourthNode = new FirstIssueInfo("4" ,6, 1.2, 22);
        FirstIssueInfo fifthNode = new FirstIssueInfo("5" ,9, 0.7, 30);
        FirstIssueInfo sixthNode = new FirstIssueInfo("6" ,5, 1.5, 26);
        FirstIssueInfo seventhNode = new FirstIssueInfo("7" ,6, 1, 24);
        FirstIssueInfo eightsNode = new FirstIssueInfo("8" ,10, 0.4, 32);

        FirstIssueInfo[] firstIssueInfos = { firstNode, secondNode,thirdNode, fourthNode, fifthNode, sixthNode, seventhNode, eightsNode};

        foreach (var issueInfo in firstIssueInfos)
        {
            issueInfo.Output();
        }

        // GOD FORGIVE ME FOR WHAT I'M ABOUT TO DO
        OtherIssueInfo firstOtherNode = new OtherIssueInfo("9", 0.4, 10, 28, 1.3);
        OtherIssueInfo secondOtherNode = new OtherIssueInfo("10", 0.6, 7, 18, 1.3);
        OtherIssueInfo thirdOtherNode = new OtherIssueInfo("9", 0.8, 8, 20, 1.3);
        OtherIssueInfo fourthOtherNode = new OtherIssueInfo("11", 0.5, 6, 24, 1.1);
        OtherIssueInfo fifthOtherNode = new OtherIssueInfo("10", 0.4, 9, 28, 1.3);
        OtherIssueInfo sixthOtherNode = new OtherIssueInfo("12", 0.7, 5, 25, 1.2);
        OtherIssueInfo seventhOtherNode = new OtherIssueInfo("11", 0.5, 6, 30, 1.1);
        OtherIssueInfo eighthOtherNode = new OtherIssueInfo("12", 0.9, 10, 22, 1.2);
        OtherIssueInfo ninthOtherNode = new OtherIssueInfo("13", 0.8, 10, 25, 3.2);
        OtherIssueInfo tenthOtherNode = new OtherIssueInfo("13", 0.5, 7, 30, 3.2);
        OtherIssueInfo eleventhOtherNode = new OtherIssueInfo("13", 0.6, 8, 26, 3.2);
        OtherIssueInfo twelvethOtherNode = new OtherIssueInfo("13", 0.4, 6, 32, 3.2);
        OtherIssueInfo thirteenthOtherNode = new OtherIssueInfo("14", 0.5, 9, 28, 4);
        OtherIssueInfo fourteenthOtherNode = new OtherIssueInfo("14", 0.7, 5, 22, 4);
        OtherIssueInfo fifteenthOtherNode = new OtherIssueInfo("14", 0.6, 6, 24, 4);
        OtherIssueInfo sixteenthOtherNode = new OtherIssueInfo("14", 0.8, 10, 20, 4);
        
        OtherIssueInfo[] otherIssueInfos = {firstOtherNode, secondOtherNode, thirdOtherNode, fourthOtherNode, fifthOtherNode, sixthOtherNode, seventhOtherNode, eighthOtherNode, ninthOtherNode, tenthOtherNode, eleventhOtherNode, twelvethOtherNode, thirteenthOtherNode,  fourteenthOtherNode,fifteenthOtherNode, sixteenthOtherNode };
        ProcessIssueInfos(otherIssueInfos);
    }

    public static void ProcessIssueInfos(OtherIssueInfo[] issues)
    {
        Array.Sort(issues);

        Dictionary<OtherIssueInfo, double> RForEach = new Dictionary<OtherIssueInfo, double>();

        foreach (OtherIssueInfo issue in issues)
        {
            if (RForEach.ContainsKey(issue))
            {           
                var currentSum = RForEach[issue];

                double timeNeeded = currentSum + (issue._timeNeeded / issue._agregation);                

                RForEach[issue] = timeNeeded;
            }
            else
            {          
                double newTimeNeeded = issue._intensity * issue._timeNeeded / issue._agregation;
                RForEach.Add(issue, newTimeNeeded);
            }
        }

        Dictionary<OtherIssueInfo, double[]> waitingTime = new Dictionary<OtherIssueInfo, double[]>();

        foreach (OtherIssueInfo issue in issues)
        {
            if (waitingTime.ContainsKey(issue))
            {                
                var currentSum = waitingTime[issue];

                double waitingTimeValue = currentSum[0] + (issue._intensity * Math.Pow(issue._timeNeeded, 2)) / 2 * (1 - RForEach[issue]);
                double spendings = currentSum[1] + (issue._intensity * 22 * issue._spendings / issue._agregation);

                double[] updatedValues = { waitingTimeValue, spendings };

                waitingTime[issue] = updatedValues;
            }
            else
            {              
                double newWaitingTimeValue = (Math.Pow(issue._timeNeeded, 2)) / 2 * (1 - RForEach[issue]);
                double newSpendings = issue._intensity * issue._spendings / issue._agregation;

                double[] newVal = {newWaitingTimeValue, newSpendings};
                waitingTime.Add(issue, newVal);
            }
        }

        foreach (var info in waitingTime)
        {
            Console.WriteLine($"Node: {info.Key._name} \n Waiting time {info.Value[0]} \n Spendings: {info.Value[1]}");
        }
    }
}
class FirstIssueInfo
{
    public string _name;    
    public double _intensity;
    public double _timeNeeded;
    public int _spendings;
    
    public FirstIssueInfo(string name, double intensity,double timeNeeded, int spendings)
    {
        _name = name;
        _intensity = intensity / 22;
        _timeNeeded = timeNeeded;
        _spendings = spendings;
    }

    public void Output()
    {
        Console.WriteLine($"Node: {_name} \n Waiting time: {CalculateWaitingTime()} \n Spendings: {CalculateSpendings()}");
    }

    private double CalculateWaitingTime()
    {
        return (double)(((_intensity) * _timeNeeded) / (1 / _timeNeeded - _intensity));
    }

    private int CalculateSpendings()
    {
        return Convert.ToInt32(_intensity * 22 * _spendings);
    }
}

class OtherIssueInfo : IComparable<OtherIssueInfo>
{
    public string _name;   
    public double _timeNeeded;
    public double _intensity;
    public int _spendings;
    public double _agregation;

    public OtherIssueInfo(string name, double timeNeeded, double intensity, int spendings, double agregation)
    {
        _name = name;
        _intensity = intensity / 22;
        _timeNeeded = timeNeeded;
        _spendings = spendings;
        _agregation = agregation;
    }

    public int CompareTo(OtherIssueInfo that)
    {
        return Convert.ToInt32(_name).CompareTo(Convert.ToInt32(that._name));
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        OtherIssueInfo other = (OtherIssueInfo)obj;
        return _name.Equals(other._name);
    }

    public override int GetHashCode()
    {
        return _name.GetHashCode();
    }
}