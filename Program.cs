using System;

namespace projekt_aplikacja_treningowa
{

    class Program // nr 0
    {
        static void Main(string[] args)
        {
            User user = new User("Michal", 221);

            Navigation navigation = new Navigation();

            navigation.displayMenu();

            while (true)
            { // nieskonczona pętla menu
                navigation.evaluateMenuSelection(user);
                navigation.displayMenu();
            }
        }
    }

    public class Generator
    { // nr 1.

        private User user;
        private String hardnessLvl; // poziom trudności
        private String target; // cel treningowy

        public Generator(ref User user, string hardnessLvl, string target)
        {
            this.user = user;
            this.hardnessLvl = hardnessLvl;
            this.target = target;
        }

        public void generateTrainingPlan() //generowanie treningow
        {
            double percentage = 0;
            Input currentForm = user.getCurrentForm();


            switch (this.hardnessLvl) //wyrażenie to umożliwia oszacowanie pojedynczego wyrażenia z listy wyrażeń
                                      //kandydowania na podstawie dopasowania wzorca switch do wyrażenia wejściowego
            {
                case "easy":
                    percentage = 0.25;
                    break;
                case "normal":
                    percentage = 0.50;
                    break;
                case "hard":
                    percentage = 0.70;
                    break;
            }

            switch (this.target)
            {
                case "strength":
                    Console.WriteLine("Wybrano trening na siłę  ");
                    Console.Write("Dzień x - 5 serii po ");
                    Console.Write(Math.Floor(percentage * currentForm.getMaxPushups()));
                    Console.WriteLine(" pompek");
                    Console.Write("Dzień x - 5 serii po ");
                    Console.Write(Math.Floor(percentage * currentForm.getMaxPullUps()));
                    Console.WriteLine(" podciagnieć");
                    break;
                case "agillity":
                    Console.WriteLine("Wybrano trening na zręczność  ");
                    Console.Write("Dzień x - 5 serii po ");
                    Console.Write(Math.Floor(percentage * currentForm.getMaxPushups()));
                    Console.WriteLine(" pompek");
                    Console.Write("Dzień x - 3 serie biegu po ");
                    Console.Write(Math.Floor(percentage * currentForm.getFiveKilometersTime()));
                    Console.WriteLine(" minut");
                    break;
                case "general":
                    Console.WriteLine("Wybrano trening ogólny ");
                    Console.Write("Dzień x - 5 serii biegu po ");
                    Console.Write(Math.Floor(percentage * currentForm.getFiveKilometersTime()));
                    Console.WriteLine(" minut");
                    break;
                default:
                    break;

            }
        }
    }
    public class Navigation // klasa odpowiedzialna za nawigację po menu
    {
        public void displayMenu()
        {
            Console.WriteLine(" ");
            Console.WriteLine("1. - Wyświetl ranking użytkownika");
            Console.WriteLine("2. - Generuj plan treningowy użytkownika");
            Console.WriteLine("3. - Dodaj wyniki sprawdzianu");
            Console.WriteLine("4. - Wyśtwietl progres użytkownika");
            Console.WriteLine("5. - Wyczyść widok");
            Console.WriteLine("6. - Wyczyść historię użytkownika");
        }

        public void evaluateMenuSelection(User user)
        {
            //dane które podaje w menu 1-6

            int selection = Int32.Parse(Console.ReadLine());

            switch (selection)
            {
                case 1:
                    user.displayCurrentRank();
                    break;
                case 2:
                    String target; //cel treningowy
                    String hardnessLvl; // poziom trudności

                    Console.Write("Podaj poziom trudności (easy/normal/hard)"); //trzy poziomy trudności
                    Console.WriteLine();
                    hardnessLvl = Console.ReadLine();
                    Console.Write("Podaj cel treningu (strength/agillity/general)"); //trzy rodzaje treningu
                    Console.WriteLine();
                    target = Console.ReadLine();

                    Console.Clear();

                    Generator generator = new Generator(ref user, hardnessLvl, target); //przeciążony konstruktor
                    generator.generateTrainingPlan();

                    break;
                case 3:
                    Console.Clear();
                    displayTestSubmenu(); // dodanie wynikow sprawdzianu
                    evaluateSubmenuSelection(user);
                    break;
                case 4:
                    user.displayProgress(); // wyswitlanie progresu
                    break;
                case 5:
                    Console.Clear(); //czyszczenie konsoli
                    break;
                case 6:
                    user.clearHistory(); //czyszczenie całej historii treningowej
                    break;
                default:
                    break;
            }
        }
        public void displayTestSubmenu() //menu dodania wynikow sprawdzianu
        {
            Console.Clear();
            Console.WriteLine("1 - Podaj wszystkie dane");
            Console.WriteLine("2 - Podaj wagę");
            Console.WriteLine("3 - Podaj wyniki testu pompek");
            Console.WriteLine("4 - Podaj wyniki testu na drążki");
            Console.WriteLine("5 - Podaj czas biegu na 5 km");
        }
        public User evaluateSubmenuSelection(User userIn)
        {
            User user = userIn;

            int selection = Int32.Parse(Console.ReadLine());

            switch (selection) // pytania odnośnie danych
            {

                case 1:
                    user.askAboutResults("all"); // wszytskich danych

                    break;
                case 2:
                    user.askAboutResults("weight");  //wagi

                    break;
                case 3:
                    user.askAboutResults("pushups"); //ilości pąpek

                    break;
                case 4:
                    user.askAboutResults("pullups"); //podciągnięć

                    break;
                case 5:
                    user.askAboutResults("5km run time"); //czasu biegu na 5km

                    break;
                default:
                    break;

            }
            return user;
        }
    }
    public class User
    { //  pseudostruktura

        private string name;
        private int ID = 123;
        private Rank[] status = new Rank[20];
        private int iterator = 0;

        Commentator commentator = new Commentator();

        public ref Input getCurrentForm()
        {

            return ref this.status[this.iterator - 1].getInput();
        }

        public ref int getIterator()
        {

            return ref iterator;
        }

        public void clearHistory()
        {
            iterator = 0; //Metoda iteratora wykonuje niestandardową iterację na kolekcji.
            Rank[] status = new Rank[20];

        }
        public User(String name, int ID)
        {
            this.name = name;
            this.ID = ID;
        }

        public void askAboutResults(String result)
        {
            Input input = new Input();

            if (iterator != 0)
            {
                input = status[iterator - 1].getInput();
            }

            switch (result)
            {
                case "all":

                    if (iterator < 20)
                    {
                        Console.Write("Podaj wzrost :");
                        input.setHeight(Int32.Parse(Console.ReadLine()));

                        Console.Write("Podaj wagę :");
                        input.setWeight(Int32.Parse(Console.ReadLine()));

                        Console.Write("Podaj maksymalną liczbę pompek :");
                        input.setMaxPushups(Int32.Parse(Console.ReadLine()));

                        Console.Write("Podaj maksymalną liczbę podciągnieć na drążku :");
                        input.setMaxPullUps(Int32.Parse(Console.ReadLine()));

                        Console.Write("Podaj czas biegu na 5km :");
                        input.setFiveKilometersTime(Int32.Parse(Console.ReadLine()));

                        Console.Write("Podaj wydolność oddechową :");
                        input.setRespiratoryEfficiency(Int32.Parse(Console.ReadLine()));

                        status[iterator] = new Rank(ref input);
                        iterator++;
                    }
                    else
                    {
                        iterator = 0;
                        Console.WriteLine("Gratulacje, oto statystyki postępu z ostatnich 20 treningów :");
                        Console.WriteLine("Zalecana jest tygodniowa przerwa, nie przemęczaj się.");
                    }
                    break;

                case "weight":
                    Console.Write("Podaj wagę :");
                    input.setWeight(Int32.Parse(Console.ReadLine()));
                    status[iterator] = new Rank(ref input);
                    iterator++;
                    break;
                case "pushups":
                    Console.Write("Podaj maksymalną liczbę pompek :");
                    input.setMaxPushups(Int32.Parse(Console.ReadLine()));
                    status[iterator] = new Rank(ref input);
                    iterator++;
                    break;
                case "pullups":
                    Console.Write("Podaj maksymalną liczbę podciągnięć :");
                    input.setMaxPullUps(Int32.Parse(Console.ReadLine()));
                    status[iterator] = new Rank(ref input);
                    iterator++;
                    break;
                case "5km time":
                    Console.Write("Podaj czas na 5 km :");
                    input.setFiveKilometersTime(Int32.Parse(Console.ReadLine()));
                    status[iterator] = new Rank(ref input);
                    iterator++;
                    break;
                case "resporatory":
                    Console.Write("Podaj wydolność oddechową:");
                    input.setRespiratoryEfficiency(Int32.Parse(Console.ReadLine()));
                    status[iterator] = new Rank(ref input);
                    iterator++;
                    break;
                default:
                    Console.WriteLine("debug - none option selected");
                    status[iterator] = new Rank(ref input);
                    iterator++;
                    break;

            }
        }

        public void displayCurrentRank()
        {

            if (this.iterator == 0)
            {
                Console.WriteLine("Najpierw dodaj dane użytkownika"); //tylko dodanie danych zezwoli nam na otrzymanie wynikow
            }
            else
            { //otrzymane wyniki
                Console.WriteLine(" ");
                Console.WriteLine("Obecny poziom użytkownika :"); //poziom ogolny
                Console.WriteLine(" ");

                Console.Write("Twój poziom BMI to : "); // poziom BMI
                Console.WriteLine(status[iterator - 1].getBMI());

                Console.Write("Twój poziom siły to : "); // poziom sily
                Console.WriteLine(status[iterator - 1].getStrength());

                Console.Write("Twój poziom zwinności to : "); //poziam zwinnosci
                Console.WriteLine(status[iterator - 1].getAgillity());

                Console.Write("Twój poziom wydajności to : "); //poziom wydajnosci
                Console.WriteLine(status[iterator - 1].getEfficiency());
                Console.WriteLine(" ");

                commentator.commentBMIValue(this.status[this.iterator - 1].getBMI());
                commentator.commentStrengthValue(this.status[this.iterator - 1].getStrength());
                commentator.commentAgillityValue(this.status[this.iterator - 1].getAgillity());
                commentator.commentEfficiencyValue(this.status[this.iterator - 1].getEfficiency());
                Console.WriteLine(" ");
            }

        }

        public void displayProgress() // menu progresu uzytkownika
        {
            double change;

            if (iterator == 0 | iterator == 1)
            {   //bez wprowadzenia danych treningowych nie poda nam historii
                Console.WriteLine("Nie posiadasz jeszcze historii treningowej");
            }
            else
            {
                double[] BMI = new double[iterator];
                double[] Strength = new double[iterator];
                double[] Agillity = new double[iterator];
                double[] Efficiency = new double[iterator];

                Console.Write("Ilość wykonanych treningow ");
                Console.Write(iterator);
                Console.WriteLine(" ");

                Console.Write("Zmiana poziomu BMI : ");
                change = status[0].getBMI() - status[iterator - 1].getBMI();
                Console.WriteLine(change);
                commentator.commentBMIChange(change);

                Console.Write("Zmiana poziomu sily : ");
                change = status[iterator - 1].getStrength() - status[0].getStrength();
                Console.WriteLine(change);
                commentator.commentStrengthChange(change);

                Console.Write("Zmiana poziomu zwinnosci : ");
                change = status[iterator - 1].getEfficiency() - status[0].getEfficiency();
                Console.WriteLine(change);
                commentator.commentEfficiencyChange(change);

                Console.Write("Zmiana poziomu wydajnosci : ");
                change = status[iterator - 1].getAgillity() - status[0].getAgillity();
                Console.WriteLine(change);
                commentator.commentAgillityChange(change);
            }
        }

    }

    public class Input
    {

        private int weight = 0;
        private int height = 0;
        private int maxPushups = 0;
        private int maxPullUps = 0;
        private int respiratoryEfficiency = 0;
        private int fiveKilometersTime = 0;

        public void setWeight(int weightIn)
        {
            weight = weightIn;
        }

        public ref int getWeight()
        {
            return ref this.weight;
        }

        public void setHeight(int heightIn)
        {
            height = heightIn;
        }

        public ref int getHeight()
        {
            return ref this.height;
        }

        public void setMaxPushups(int maxPushupsIn)
        {
            maxPushups = maxPushupsIn;
        }

        public ref int getMaxPushups()
        {
            return ref this.maxPushups;
        }

        public void setMaxPullUps(int maxPullUpsIn)
        {
            maxPullUps = maxPullUpsIn;
        }

        public ref int getMaxPullUps()
        {
            return ref this.maxPullUps;
        }
        public void setRespiratoryEfficiency(int respiratoryEfficiencyIn)
        {
            respiratoryEfficiency = respiratoryEfficiencyIn;
        }

        public ref int getRespiratoryEfficiency()
        {
            return ref this.respiratoryEfficiency;
        }
        public void setFiveKilometersTime(int fiveKilometersTimeIn)
        {
            fiveKilometersTime = fiveKilometersTimeIn;
        }

        public ref int getFiveKilometersTime()
        {
            return ref this.respiratoryEfficiency;
        }
    }
    public class Commentator
    {

        public void commentBMIValue(double BMI) //jak oceniamy wartosci BMI
        {
            if (BMI > 30)
            {
                Console.Write("Otyłość: ");
            }
            else if (BMI < 30 && BMI > 25)
            {
                Console.Write("Nadwaga: ");
            }
            else if (BMI < 25 && BMI > 18)
            {
                Console.Write("Prawidłowa waga: ");
            }
            else if (BMI < 18 && BMI > 15)
            {
                Console.Write("Wychudzenie: ");
            }
            else if (BMI < 15)
            {
                Console.Write("Wygłodzenie: ");
            }

            Console.WriteLine(BMI);
        }

        public void commentStrengthValue(double Strength) // jak oceniamy wartosci sily
        {
            if (Strength > 50)
            {
                Console.Write("Twoja sila utrzymuje sie na wysokim poziomie: ");
            }
            else if (Strength < 50 && Strength > 25)
            {
                Console.Write("Twoja sila utrzymuje sie na srednim poziomie ");
            }
            else if (Strength < 25)
            {
                Console.Write("Popracuj jeszcze, twoja sila utrzymuje sie na niskim poziomie: ");
            }

            Console.WriteLine(Strength);
        }

        public void commentAgillityValue(double Agillity) //jak oceniamy wartosci zwinnosci
        {
            if (Agillity > 200)
            {
                Console.Write("Twoja zwinnosc utrzymuje sie na wysokim poziomie: ");
            }
            else if (Agillity < 200 && Agillity > 100)
            {
                Console.Write("Twoja zwinnosc utrzymuje sie na tym srednim poziomie: ");
            }
            else if (Agillity < 100)
            {
                Console.Write("Popracuj jeszcze, twoja zwinnosc jest na niskim poziomie: ");
            }

            Console.WriteLine(Agillity);
        }

        public void commentEfficiencyValue(double Efficiency) // jak oceniamy wartosc wydajnosci
        {
            if (Efficiency > 300)
            {
                Console.Write("Twoja wydajnosc utrzymuje sie na wysokim poziomie: ");
            }
            else if (Efficiency < 300 && Efficiency > 150)
            {
                Console.Write("Twoja wydajnosc utrzymuje sie na tym srednim poziomie: ");
            }
            else if (Efficiency < 150)
            {
                Console.Write("Popracuj jeszcze, twoja wydajnosc jest na niskim poziomie: ");
            }
            Console.WriteLine(Efficiency);
        }

        public void commentBMIChange(double change) // informacje odnosnie zmiany progresu dla poszczegolnych wartosci
        {
            if (change < 0)
            {
                Console.WriteLine("Twoja waga sie zwiekszyla");
            }
            else if (change > 0)
            {
                Console.WriteLine("Twoja waga sie zmniejszyla");
            }
        }

        public void commentStrengthChange(double change)
        {
            if (change > 0)
            {
                Console.WriteLine("Twoja sila wzrosla");
            }
            else if (change < 0)
            {
                Console.WriteLine("Twoja sila zmalala");
            }
        }

        public void commentAgillityChange(double change)
        {
            if (change > 0)
            {
                Console.WriteLine("Twoja zwinnosc wzrosla");
            }
            else if (change < 0)
            {
                Console.WriteLine("Twoja zwinnosc zmalala");
            }
        }

        public void commentEfficiencyChange(double change)
        {
            if (change > 0)
            {
                Console.WriteLine("Twoja zrecznosc wzrosla");
            }
            else if (change < 0)
            {
                Console.WriteLine("Twoja zrecznosc zmalala");
            }
        }
    }
    public class Rank  //sposoby oblicznia poszczegolnych wartosci
    {
        private Input input;

        private double BMI;
        private double Strength;
        private double Agillity;
        private double Efficiency;

        public ref double getBMI()
        {
            return ref this.BMI;
        }

        public ref double getStrength()
        {
            return ref this.Strength;
        }
        public ref double getAgillity()
        {
            return ref this.Agillity;
        }

        public ref double getEfficiency()
        {
            return ref this.Efficiency;
        }
        public Rank(ref Input ins)
        {
            this.input = ins;

            BMI = getBMI(input.getWeight(), input.getHeight());
            Strength = getStrength(input.getMaxPushups(), input.getMaxPullUps());
            Agillity = getAgillity(input.getFiveKilometersTime(), input.getHeight());
            Efficiency = getEfficiency(input.getRespiratoryEfficiency());
        }
        public ref Input getInput()
        {
            return ref this.input;
        }


        double getBMI(int weight, int height)
        {
            double BMI = ((double)weight / (((double)height / (double)100) * ((double)height / (double)100)));

            return BMI;
        }

        double getStrength(int maxPushUps, int maxPullUps)
        {
            int pushUpWeight = 1;
            int pullUpWeight = 2;

            return pushUpWeight * maxPushUps + pullUpWeight * maxPullUps;
        }

        double getAgillity(int fiveKilometersTime, int height)
        {
            int heightWeight = 1;
            int fiveKilometersTimeWeight = 2;

            return fiveKilometersTimeWeight * (1 / (double)fiveKilometersTime) + heightWeight * height;
        }

        double getEfficiency(int respiratoryEfficiency)

        {
            int agillityWeight = 1;
            int strengthWeight = 2;
            int respiratoryEfficiencyWeight = 1;

            double efficiency = (this.Agillity * agillityWeight + this.Strength * strengthWeight) + respiratoryEfficiencyWeight * respiratoryEfficiency;
            return efficiency;

        }
    };
};