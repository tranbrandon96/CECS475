// Name: Brandon Tran
// CECS 475: Phuong Nguyen
// Date: 1/29/2020
// Lab Assignment 2: Factory Pattern

using System;

namespace CECS475Lab2_2
{
    class Program
    {
        abstract class HealthPlan
        {
            protected string _planType;
            protected double _annualCharge;
            protected double _deductionAmount;
            public abstract string planType { get; }
            public abstract double annualCharge { get; set; }
            public abstract double deductionAmount { get; set; }
        }

        class HMO : HealthPlan
        {
            public HMO()
            {
                _planType = "HMO";
            }
            public override string planType { get => _planType; }
            public override double annualCharge { get => _annualCharge; set => _annualCharge = value; }
            public override double deductionAmount { get => _deductionAmount; set => _deductionAmount = value; }
        }

        class PPO : HealthPlan
        {
            public PPO()
            {
                _planType = "PPO";
            }
            public override string planType { get => _planType; }
            public override double annualCharge { get => _annualCharge; set => _annualCharge = value; }
            public override double deductionAmount { get => _deductionAmount; set => _deductionAmount = value; }
        }

        class ObamaCare : HealthPlan
        {
            public ObamaCare()
            {
                _planType = "ObamaCare";
            }
            public override string planType { get => _planType; }
            public override double annualCharge { get => _annualCharge; set => _annualCharge = value; }
            public override double deductionAmount { get => _deductionAmount; set => _deductionAmount = value; }
        }

        //-------------------------------------------------------------------------
        abstract class HealthPlanFactory
        {
            protected double _annualCharge;
            protected double _deductionAmount;

            public abstract HealthPlan getPlan();

        }

        class HMOFactory : HealthPlanFactory
        {
            public override HealthPlan getPlan()
            {
                HealthPlan HMO; 
                return new HMO();
            }

        }

        class PPOFactory : HealthPlanFactory
        {
            public override HealthPlan getPlan()
            {
                HealthPlan PPO;
                return new PPO();
            }
        }

        class ObamaCareFactory: HealthPlanFactory
        {
            public override HealthPlan getPlan()
            {
                HealthPlan ObamaCare;
                return new ObamaCare();
            }
        }

        static void Main(string[] args)
        {
            bool isTrue = true;
            do
            {
                Console.WriteLine("\nEnter a healthcare plan: " +
                          "1) HMO" +
                          "\t2) PPO" +
                          "\t3) ObamaCare");
                switch(Console.ReadLine())
                {
                    case "1":
                        HMOFactory hmo = new HMOFactory();
                        HealthPlan hmoPlan = hmo.getPlan();
                        Console.WriteLine($"Plan Type: HMO");
                        Console.WriteLine($"Annual Charge: ${hmoPlan.annualCharge = 1000.00}");
                        Console.WriteLine($"Deduction Amount: ${hmoPlan.deductionAmount = 200.00}");
                        break;
                    case "2":
                        PPOFactory ppo = new PPOFactory();
                        HealthPlan ppoPlan = ppo.getPlan();
                        Console.WriteLine($"Plan Type: PPO");
                        Console.WriteLine($"Annual Charge: ${ppoPlan.annualCharge = 1200.00}");
                        Console.WriteLine($"Deduction Amount: ${ppoPlan.deductionAmount = 150.00}");
                        break;
                    case "3":
                        ObamaCareFactory obamaCare = new ObamaCareFactory();
                        HealthPlan obamaCarePlan = obamaCare.getPlan();
                        Console.WriteLine($"Plan Type: ObamaCare");
                        Console.WriteLine($"Annual Charge: ${obamaCarePlan.annualCharge = 1400.00}");
                        Console.WriteLine($"Deduction Amount: ${obamaCarePlan.deductionAmount = 100.00}");
                        break;
                    default:
                        isTrue = false;
                        break;
                }
            } while (isTrue != false);
       
            
        }
    }
}
