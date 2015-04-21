/* Elina Karppinen
 * C# ja .NET harjoitustyö 1. about 28h
 * 
 * Puuttuvia:
 * -Vuoden huomioon ottaminen.
 * -Muokkaus.
 * 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Pistelaskuri
{
    public partial class Form1 : Form
    {
        List<Dog> dog_list = new List<Dog>();
        string dogFile = "Dogs.csv", showFile = "Shows.csv", testFile = "Tests.csv";

        public Form1()
        {
            InitializeComponent();
            //Näyttely
            comboBoxRYP.SelectedIndex = 0;
            comboBoxBIS.SelectedIndex = 0;
            radioButtonKV.Checked = true;
            radioButtonROP.Checked = true;
            comboBoxDogShow.SelectedIndex = 0;
            comboBoxBIS.Enabled = true;
            comboBoxRYP.Enabled = true;

            //Koe
            radioButtonToko.Checked = true;
            radioButton1tulos.Checked = true;
            radioButtonSij1.Checked = true;
            comboBoxDogTest.SelectedIndex = 0;
            comboBoxLkChange.SelectedIndex = 0;
            comboBoxKoulari.SelectedIndex = 0;
            comboBoxCompetition.SelectedIndex = 0;
            checkedListBoxRT_TP.Enabled = false;

            //Tulokset
            comboBoxResults.SelectedIndex = 0;

            //koira
            if (File.Exists(dogFile))
            {
                using (ReadDog reader = new ReadDog(dogFile))
                {
                    dog_list = reader.ReadDogRow();
                    foreach (Dog dog in dog_list)
                    {
                        comboBoxDogShow.Items.Add(dog.VirName + " \"" + dog.KutsName + "\"");
                        comboBoxDogTest.Items.Add(dog.VirName + " \"" + dog.KutsName + "\"");
                    }
                }
            }
        }

        Show showInfo = new Show();
        Test testInfo = new Test();
        Dog dog = new Dog();
        List<Show> show_list = new List<Show>();
        List<Test> test_list = new List<Test>();

        //Näyttely
        string showType, sijoitus, rypSija, bisSija;
        int sijoitusPisteet = 0, rypSijoitus = 0, bisSijoitus = 0;
        
        private void radioButtonKV_CheckedChanged(object sender, EventArgs e)
        {
            showType = "kv";
            comboBoxBIS.Enabled = true;
            comboBoxRYP.Enabled = true;
        }

        private void radioButtonRN_CheckedChanged(object sender, EventArgs e)
        {
            showType = "rn";
            comboBoxBIS.Enabled = true;
            comboBoxRYP.Enabled = true;
            //comboBoxBIS.SelectedIndex = 0;
        }

        private void radioButtonER_CheckedChanged(object sender, EventArgs e)
        {
            showType = "er";
            comboBoxBIS.Enabled = true;
            comboBoxRYP.Enabled = true;
            //comboBoxBIS.SelectedIndex = 0;
        }

        private void radioButtonColorShow_CheckedChanged(object sender, EventArgs e)
        {
            showType = "color";
            comboBoxBIS.Enabled = true;
            comboBoxRYP.Enabled = true;
            //comboBoxBIS.SelectedIndex = 0;
        }

        private void radioButtonROP_CheckedChanged(object sender, EventArgs e)
        {
            sijoitus = "ROP";
            comboBoxRYP.Enabled = true;

            if (showType == "kv")
                comboBoxBIS.Enabled = true;
            else
                comboBoxBIS.Enabled = false;
        }

        private void radioButtonVSP_CheckedChanged(object sender, EventArgs e)
        {
            sijoitus = "VSP";
            setBISRYP_disabled();
        }

        private void radioButtonPN2_CheckedChanged(object sender, EventArgs e)
        {
            sijoitus = "PN2/PU2";
            setBISRYP_disabled();
        }

        private void radioButtonPN3_CheckedChanged(object sender, EventArgs e)
        {
            sijoitus = "PN3/PU3";
            setBISRYP_disabled();
        }

        private void radioButtonPN4_CheckedChanged(object sender, EventArgs e)
        {
            sijoitus = "PN4/PU4";
            setBISRYP_disabled();
        }

        private void radioButtonOther_CheckedChanged(object sender, EventArgs e)
        {
            setBISRYP_disabled();
            sijoitus = textBoxOtherShowResult.Text;
        }

        private void comboBoxRYP_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ind;
            ind = comboBoxRYP.SelectedIndex;
            switch (ind)
            {
                case 0:
                    //valitse
                    rypSija = "";
                    break;
                case 1:
                    //RYP1
                    rypSija = "RYP1";
                    comboBoxBIS.Enabled = true;
                    break;
                case 2:
                    rypSija = "RYP2";
                    comboBoxBIS.Enabled = false;
                    comboBoxBIS.SelectedIndex = 0;
                    break;
                case 3:
                    rypSija = "RYP3";
                    comboBoxBIS.Enabled = false;
                    comboBoxBIS.SelectedIndex = 0;
                    break;
                case 4:
                    rypSija = "RYP4";
                    comboBoxBIS.Enabled = false;
                    comboBoxBIS.SelectedIndex = 0;
                    //RYP4
                    break;
            }
        }

        private void comboBoxBIS_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ind;
            ind = comboBoxBIS.SelectedIndex;
            switch (ind)
            {
                case 0:
                    //valitse
                    bisSija = "";
                    break;
                case 1:
                    //BIS1
                    bisSija = "BIS1";
                    break;
                case 2:
                    bisSija = "BIS2";
                    break;
                case 3:
                    bisSija = "BIS3";
                    break;
                case 4:
                    //BIS4
                    bisSija = "BIS4";
                    break;
            }
        }

        private void buttonAddShow_Click(object sender, EventArgs e)
        {
            int points = 0;
            string date, place, placing;

            if (comboBoxDogShow.SelectedIndex == 0)
                MessageBox.Show("Valitse koira. Jos koiria ei ole valittavana" + System.Environment.NewLine +
                    "käy lisäämässä koira \"Koira\"-välilehdellä.");
            else if (textBoxShowPlace.Text == "")
                MessageBox.Show("Lisää näyttelypaikka.");
            else
            {
                getShowPoints();
                points = GetExtraShowPoints();
                points = points + sijoitusPisteet + rypSijoitus + bisSijoitus;
                textBoxShowPoints.Text = "" + points;

                DateTime dt = dateTimePickerShow.Value;
                date = dt.ToString("dd-MM-yyyy");
                place = textBoxShowPlace.Text;
                placing = sijoitus + " " + rypSija + " " + bisSija;

                Dog doggi = dog_list.ElementAt((comboBoxDogShow.SelectedIndex - 1));

                showInfo = new Show(date, place, showType, placing, points, doggi);
                show_list.Add(showInfo);

                using (WriteShow writer = new WriteShow(showFile))
                {
                    writer.WriteShowRow(showInfo);
                }
            }
        }

        public void setBISRYP_disabled()
        {
            comboBoxBIS.SelectedIndex = 0;
            comboBoxBIS.Enabled = false;
            comboBoxRYP.SelectedIndex = 0;
            comboBoxRYP.Enabled = false;
        }

        public void getShowPoints()
        {
            bisSijoitus = 0;

            if (showType == "kv")
            {
                if (sijoitus == "ROP")
                {
                    sijoitusPisteet = 20;

                    if (rypSija == "RYP1")
                    {
                        rypSijoitus = 20;

                        if (bisSija == "BIS1")
                            bisSijoitus = 20;
                        else if(bisSija == "BIS2")
                            bisSijoitus = 17;
                        else if(bisSija == "BIS3")
                            bisSijoitus = 14;
                        else if (bisSija == "BIS4")
                            bisSijoitus = 11;
                        else
                            bisSijoitus = 0;
                    }
                    else if (rypSija == "RYP2")
                        rypSijoitus = 17;
                    else if (rypSija == "RYP3")
                        rypSijoitus = 14;
                    else if (rypSija == "RYP4")
                        rypSijoitus = 11;
                    else
                        rypSijoitus = 0;
                }
                else if (sijoitus == "VSP")
                    sijoitusPisteet = 15;
                else if (sijoitus == "PN2/PU2")
                    sijoitusPisteet = 10;
                else if (sijoitus == "PN3/PU3")
                    sijoitusPisteet = 7;
                else if (sijoitus == "PN4/PU4")
                    sijoitusPisteet = 4;
                else
                    sijoitusPisteet = 0;


            }
            else //other shows
            {
                if (sijoitus == "ROP")
                {
                    sijoitusPisteet = 12;

                    if (rypSija == "RYP1")
                        rypSijoitus = 10;
                    else if (rypSija == "RYP2")
                        rypSijoitus = 8;
                    else if (rypSija == "RYP3")
                        rypSijoitus = 6;
                    else if (rypSija == "RYP4")
                        rypSijoitus = 4;
                    else
                        rypSijoitus = 0;
                }
                else if (sijoitus == "VSP")
                    sijoitusPisteet = 8;
                else if (sijoitus == "PN2/PU2")
                    sijoitusPisteet = 6;
                else if (sijoitus == "PN3/PU3")
                    sijoitusPisteet = 4;
                else if (sijoitus == "PN4/PU4")
                    sijoitusPisteet = 2;
                else
                    sijoitusPisteet = 0;
            }

        }

        public int GetExtraShowPoints()
        {
            int dogLkm = 0, extraPoints = 0, sexLkm = 0;
            if (int.TryParse(textBoxDogLKM.Text, out dogLkm) && dogLkm >= 0)
            {
                if (int.TryParse(textBoxSexLKM.Text, out sexLkm) && sexLkm >= 0)
                {
                    if (sijoitus == "ROP")
                    {
                        extraPoints = dogLkm / 5;
                        //1. piste jokaista 5:ttä ilmoitettua koiraa kohti
                    }
                    else if (sijoitus == "VSP" || sijoitus == "PN2/PU2" ||
                        sijoitus == "PN3/PU3" || sijoitus == "PN4/PU4")
                    {
                        extraPoints = sexLkm / 5;
                        //1. piste jokaista 5:ttä ilmoitettua saman sukupuolen koiraa kohti
                    }
                    else
                        extraPoints = 0;
                }
                else
                    MessageBox.Show("Koirien määrän pitää olla kokonaisluku!" + System.Environment.NewLine + "Tarkista määrät ja kokeile uudelleen!");
            }
            else
                MessageBox.Show("Koirien määrän pitää olla kokonaisluku!" + System.Environment.NewLine + "Tarkista määrät ja kokeile uudelleen!");
            return extraPoints;
        }

        //Koira
        string testType, testClass;

        private void buttonAddDog_Click(object sender, EventArgs e)
        {
            if (textBoxVirName.Text == "")
                MessageBox.Show("Anna koiran virallinen nimi tai kirjoita kutsumanimi.");
            else
            {
                if (textBoxKutsName.Text == "")
                    MessageBox.Show("Anna koiran kutsumanimi.");
                else
                {
                    Dog doggi = new Dog();
                    doggi.VirName = textBoxVirName.Text;
                    doggi.KutsName = textBoxKutsName.Text;

                    comboBoxDogShow.Items.Add(textBoxVirName.Text + " \"" + textBoxKutsName.Text + "\"");
                    comboBoxDogTest.Items.Add(textBoxVirName.Text + " \"" + textBoxKutsName.Text + "\"");
                    dog_list.Add(doggi);

                    using (WriteDog writer = new WriteDog(dogFile))
                    {
                        writer.WriteDogRow(doggi);
                    }
                }
            }
        }

        private void buttonGetDog_Click(object sender, EventArgs e)
        {
            textBoxDogList.Text = "";
            foreach (Dog doggi in dog_list)
            {
                textBoxDogList.Text = textBoxDogList.Text + doggi.VirName + " \"" + doggi.KutsName + "\""
                    + System.Environment.NewLine;
            }
        }

        //Koe
        private void radioButtonToko_CheckedChanged(object sender, EventArgs e)
        {
            testType = "TOKO";
            radioButtonMolli.Enabled = false;
            radioButtonALO.Enabled = true;
            radioButtonAVO.Enabled = true;
            radioButtonVOI.Enabled = true;
            radioButtonEVL.Enabled = true;
            radioButton2tulos.Enabled = true;
            radioButton3tulos.Enabled = true;
            checkedListBoxRT_TP.Enabled = false;
            if (radioButtonMolli.Checked == true)
            {
                radioButtonMolli.Checked = false;
                }
        }

        private void radioButtonRT_CheckedChanged(object sender, EventArgs e)
        {
            testType = "Rally-Toko";
            radioButtonMolli.Enabled = false;
            radioButtonALO.Enabled = true;
            radioButtonAVO.Enabled = true;
            radioButtonVOI.Enabled = true;
            radioButtonEVL.Enabled = true;
            radioButton2tulos.Enabled = false;
            radioButton3tulos.Enabled = false;
            if (radioButton2tulos.Checked == true || radioButton3tulos.Checked == true)
                radioButton1tulos.Checked = true;
            checkedListBoxRT_TP.Enabled = true;
            if (radioButtonMolli.Checked == true)
                radioButtonMolli.Checked = false;

        }

        private void radioButtonMEJA_CheckedChanged(object sender, EventArgs e)
        {
            testType = "MEJÄ";
            radioButtonMolli.Enabled = false;
            radioButtonALO.Enabled = false;
            radioButtonAVO.Enabled = true;
            radioButtonVOI.Enabled = true;
            radioButtonEVL.Enabled = false;
            radioButton2tulos.Enabled = true;
            radioButton3tulos.Enabled = true;
            checkedListBoxRT_TP.Enabled = false;
            if (radioButtonMolli.Checked == true || radioButtonALO.Checked == true || radioButtonEVL.Checked == true)
            {
                radioButtonMolli.Checked = false;
                radioButtonALO.Checked = false;
                radioButtonEVL.Checked = false;
            }
        }
        
        private void radioButtonBH_CheckedChanged(object sender, EventArgs e)
        {
            testType = "BH";
            radioButtonMolli.Enabled = false;
            radioButtonALO.Enabled = false;
            radioButtonAVO.Enabled = false;
            radioButtonVOI.Enabled = false;
            radioButtonEVL.Enabled = false;
            radioButton2tulos.Enabled = false;
            radioButton3tulos.Enabled = false;
            radioButton1tulos.Checked = true;
            checkedListBoxRT_TP.Enabled = false;
        }

        private void radioButtonRotuToko_CheckedChanged(object sender, EventArgs e)
        {
            testType = "Rotutoko";
            radioButtonMolli.Enabled = true;
            radioButtonALO.Enabled = true;
            radioButtonAVO.Enabled = true;
            radioButtonVOI.Enabled = true;
            radioButtonEVL.Enabled = true;
            radioButton2tulos.Enabled = true;
            radioButton3tulos.Enabled = true;
            checkedListBoxRT_TP.Enabled = false;

        }

        private void buttonAddTest_Click(object sender, EventArgs e)
        {
            string date, place, testResult, sij;
            int points = 0, sPoint = 0, koepiste;
            Boolean isOk = true;

            if (comboBoxDogTest.SelectedIndex == 0)
                MessageBox.Show("Valitse koira. Jos koiria ei ole valittavana" + System.Environment.NewLine +
                    "käy lisäämässä koira \"Koira\"-välilehdellä.");
            else if (textBoxKoePlace.Text == "")
                MessageBox.Show("Lisää koepaikka.");
            else
            {
                points = getTestPoints();
                sPoint = stayPoints();
                textBoxShowTestPoints.Text = "" + (points + sPoint);

                if (radioButton1tulos.Checked == true)
                    testResult = testClass + "1";
                else if (radioButton2tulos.Checked == true)
                    testResult = testClass + "2";
                else if (radioButton3tulos.Checked == true)
                    testResult = testClass + "3";
                else
                    testResult = testClass + "-";

                if (radioButtonSij1.Checked == true)
                    sij = "1.";
                else if (radioButtonSij2.Checked == true)
                    sij = "2.";
                else if (radioButtonSij3.Checked == true)
                    sij = "3.";
                else if (radioButtonSij4.Checked == true)
                    sij = "4.";
                else if (radioButtonSij5.Checked == true)
                    sij = "5.";
                else
                    sij = textBoxMuuSij.Text;

                DateTime dt = dateTimePickerTest.Value;
                date = dt.ToString("dd-MM-yyyy");
                place = textBoxKoePlace.Text;

                isOk = int.TryParse(textBoxKoePisteet.Text, out koepiste);
                if(isOk==false)
                    koepiste = 0;
                
                Dog doggi = dog_list.ElementAt((comboBoxDogTest.SelectedIndex-1));

                Test testInfo = new Test(date, place, testType, testResult, koepiste, sij, points, doggi);
                test_list.Add(testInfo);

                using (WriteTest writer = new WriteTest(testFile))
                {
                    writer.WriteTestRow(testInfo);
                }
            }


        }

        public int getTestPoints()
        {
            int points = 0, pojo = 0;
            if (radioButtonBH.Checked == true)
            {
                if (radioButton1tulos.Checked == true)
                    points = 25;
                else
                    points = 0;
                testClass = "";
            }
            else
            {
                if (radioButtonMolli.Checked == false && radioButtonALO.Checked == false &&
                    radioButtonAVO.Checked == false && radioButtonVOI.Checked == false &&
                    radioButtonEVL.Checked == false)
                {
                    MessageBox.Show("Valitse luokka");
                }
                else
                {
                    if (radioButtonMolli.Checked == true)
                    {
                        testClass = "Mölli";
                        points = 2; //osallistuminen

                        if (radioButton1tulos.Checked == true)
                            points = points + 10;
                        else if (radioButton2tulos.Checked == true)
                            points = points + 7;
                        else if (radioButton3tulos.Checked == true)
                            points = points + 4;
                        else
                            points = 0; //Hylätystä tuloksesta ei osallistumispisteitä

                        if (radioButtonSij1.Checked == true)
                            points = points + 6;
                        else if (radioButtonSij2.Checked == true)
                            points = points + 5;
                        else if (radioButtonSij3.Checked == true)
                            points = points + 4;
                        else if (radioButtonSij4.Checked == true)
                            points = points + 3;
                        else if (radioButtonSij5.Checked == true)
                            points = points + 2;
                        else if (radioButtonSijMuu.Checked == true)
                        {
                            if(int.TryParse(textBoxMuuSij.Text, out pojo))
                            {
                                if (pojo == 6)
                                    points = points + 1;
                            }
                        }
                    }
                    else if (radioButtonALO.Checked == true)
                    {
                        testClass = "ALO";
                        points = 2; //osallistuminen

                        if (radioButton1tulos.Checked == true)
                            points = points + 15;
                        else if (radioButton2tulos.Checked == true)
                            points = points + 10;
                        else if (radioButton3tulos.Checked == true)
                            points = points + 5;
                        else
                            points = 0; //Hylätystä tuloksesta ei osallistumispisteitä

                        if (radioButtonHYL.Checked == false && radioButtonSij1.Checked == true)
                            points = points + 10;
                        else if (radioButtonHYL.Checked == false && radioButtonSij2.Checked == true)
                            points = points + 8;
                        else if (radioButtonHYL.Checked == false && radioButtonSij3.Checked == true)
                            points = points + 6;
                        else if (radioButtonHYL.Checked == false && radioButtonSij4.Checked == true)
                            points = points + 4;
                        else if (radioButtonHYL.Checked == false && radioButtonSij5.Checked == true)
                            points = points + 2;
                    }
                    else if (radioButtonAVO.Checked == true)
                    {
                        testClass = "AVO";
                        points = 4; //osallistuminen

                        if (radioButton1tulos.Checked == true)
                            points = points + 20;
                        else if (radioButton2tulos.Checked == true)
                            points = points + 15;
                        else if (radioButton3tulos.Checked == true)
                            points = points + 10;
                        else
                            points = 0; //Hylätystä tuloksesta ei osallistumispisteitä

                        if (radioButtonHYL.Checked == false && radioButtonSij1.Checked == true)
                            points = points + 15;
                        else if (radioButtonHYL.Checked == false && radioButtonSij2.Checked == true)
                            points = points + 13;
                        else if (radioButtonHYL.Checked == false && radioButtonSij3.Checked == true)
                            points = points + 11;
                        else if (radioButtonHYL.Checked == false && radioButtonSij4.Checked == true)
                            points = points + 9;
                        else if (radioButtonHYL.Checked == false && radioButtonSij5.Checked == true)
                            points = points + 7;
                    }
                    else if (radioButtonVOI.Checked == true)
                    {
                        testClass = "VOI";
                        points = 6; //osallistuminen

                        if (radioButton1tulos.Checked == true)
                            points = points + 25;
                        else if (radioButton2tulos.Checked == true)
                            points = points + 20;
                        else if (radioButton3tulos.Checked == true)
                            points = points + 15;
                        else
                            points = 0; //Hylätystä tuloksesta ei osallistumispisteitä

                        if (radioButtonHYL.Checked == false && radioButtonSij1.Checked == true)
                            points = points + 20;
                        else if (radioButtonHYL.Checked == false && radioButtonSij2.Checked == true)
                            points = points + 18;
                        else if (radioButtonHYL.Checked == false && radioButtonSij3.Checked == true)
                            points = points + 16;
                        else if (radioButtonHYL.Checked == false && radioButtonSij4.Checked == true)
                            points = points + 14;
                        else if (radioButtonHYL.Checked == false && radioButtonSij5.Checked == true)
                            points = points + 12;
                    }
                    else if (radioButtonEVL.Checked == true)
                    {
                        testClass = "EVL/MES";
                        points = 8; //osallistuminen

                        if (radioButton1tulos.Checked == true)
                            points = points + 30;
                        else if (radioButton2tulos.Checked == true)
                            points = points + 25;
                        else if (radioButton3tulos.Checked == true)
                            points = points + 20;
                        else
                            points = 0; //Hylätystä tuloksesta ei osallistumispisteitä

                        if (radioButtonHYL.Checked == false && radioButtonSij1.Checked == true)
                            points = points + 25;
                        else if (radioButtonHYL.Checked == false && radioButtonSij2.Checked == true)
                            points = points + 23;
                        else if (radioButtonHYL.Checked == false && radioButtonSij3.Checked == true)
                            points = points + 21;
                        else if (radioButtonHYL.Checked == false && radioButtonSij4.Checked == true)
                            points = points + 19;
                        else if (radioButtonHYL.Checked == false && radioButtonSij5.Checked == true)
                            points = points + 17;
                    }

                    //Lisäpisteet
                    //RT tuomaripalkinto
                    if (radioButtonRT.Checked == true && checkedListBoxRT_TP.SelectedIndex == 0)
                        points = points + 10;
                    //arvokisa
                    if (comboBoxCompetition.SelectedIndex == 1)
                        points = points + 15;
                    else if (comboBoxCompetition.SelectedIndex == 2)
                        points = points + 20;
                    else if (comboBoxCompetition.SelectedIndex == 3)
                        points = points + 25;
                    else if (comboBoxCompetition.SelectedIndex == 4)
                        points = points + 30;
                    else if (comboBoxCompetition.SelectedIndex == 5)
                        points = points + 35;
                }               
            }
            return points;
        }

        public int stayPoints()
        {
            int basePoints = dog.BasePoints;
            
            //luokkanousu
            if (comboBoxLkChange.SelectedIndex == 1)
                basePoints = basePoints + 15;
            else if (comboBoxLkChange.SelectedIndex == 2)
                basePoints = basePoints + 20;
            else if (comboBoxLkChange.SelectedIndex == 3)
                basePoints = basePoints + 25;
            //valioituminen
            if (checkedListBoxChamp.SelectedIndex==0)
                basePoints = basePoints + 50;
            //koulutustunnus
            if (comboBoxKoulari.SelectedIndex == 1)
                basePoints = basePoints + 10;
            else if (comboBoxKoulari.SelectedIndex == 2)
                basePoints = basePoints + 20;
            else if (comboBoxKoulari.SelectedIndex == 3)
                basePoints = basePoints + 30;
            else if (comboBoxKoulari.SelectedIndex == 4)
                basePoints = basePoints + 40;

            dog.BasePoints = basePoints;
            return basePoints;
        }

        //Tulokset
        private void buttonGetResults_Click(object sender, EventArgs e)
        {
            string koira, testClass;
            
            //Koe
            if (File.Exists(testFile))
            {
                using (ReadTest reader = new ReadTest(testFile))
                {
                    test_list = reader.ReadTestRow();
                }
            }

            //Näyttely
            if (File.Exists(showFile))
            {
                using (ReadShow reader = new ReadShow(showFile))
                {
                    show_list = reader.ReadShowRow();                    
                }
            }
            textBoxAllResults.Text = "";

            if (comboBoxResults.SelectedIndex == 0)
            {
                //Koe
                foreach (Test test in test_list)
                {
                    koira = test.Doggi.VirName + " \"" + test.Doggi.KutsName + " \"";

                    if (test.Type != "BH" && test.Type != "Rally-Toko")
                    {
                        textBoxAllResults.Text = textBoxAllResults.Text + koira + " " + test.Type + " " + test.Date + " "
                            + test.Place + " " + test.TestResult + " " + test.TestPoints + " pistettä, sijoitus:"
                            + test.TestSija + ", " + test.KisaPoints + " pistettä ansaittu Harrastusdoggi kisaan"
                            + System.Environment.NewLine;
                    }
                    else
                    {
                        if (test.TestResult == "1")
                            testClass = "Hyväksytty";
                        else
                            testClass = "Hylätty";
                        textBoxAllResults.Text = textBoxAllResults.Text + koira + " " + test.Type + " " + test.Date + " "
                            + test.Place + " " + testClass + " " + test.TestPoints + " pistettä, sijoitus:"
                            + test.TestSija + ". " + test.KisaPoints + " pistettä Harrastusdoggikisaan"
                            + System.Environment.NewLine;
                    }
                }
            }
            else if (comboBoxResults.SelectedIndex == 1)
            {
                //Näyttely
                foreach (Show show in show_list)
                {
                    koira = show.Doggi.VirName + " \"" + show.Doggi.KutsName + " \"";
                    textBoxAllResults.Text = textBoxAllResults.Text + koira + " " + show.Date + " " + show.Type + " "
                        + show.Place + " " + show.Placing + ". " + show.Points + " pistettä Kultadoggikisaan"
                        + System.Environment.NewLine;
                }
            }
            else if (comboBoxResults.SelectedIndex == 2)
            {
                textBoxAllResults.Text = "Harrastusdoggikisa:" + System.Environment.NewLine;
                //Harrastusdoggi

                countTestPoints(testFile);
                List<Dog> sort_list = sorting("test");
                foreach (Dog dog in dog_list)
                {
                    koira = dog.VirName + " \"" + dog.KutsName + " \"";
                    textBoxAllResults.Text = textBoxAllResults.Text + koira + ": " + dog.fullPointsTest + System.Environment.NewLine;
                }
            }
            else if (comboBoxResults.SelectedIndex == 3)
            {
                textBoxAllResults.Text = "Kultadoggikisa:" + System.Environment.NewLine;
                //Kultadoggi

                countShowPoints(showFile);
                List<Dog> sort_list = sorting("show");
                foreach (Dog dog in sort_list)
                {
                    koira = dog.VirName + " \"" + dog.KutsName + " \"";
                    textBoxAllResults.Text = textBoxAllResults.Text + koira + ": " + dog.fullPointsShow + System.Environment.NewLine;
                }
            }
        }

        public void countTestPoints(string testFile)
        {
            // Points for Harrastusdoggi competition 
            foreach (Dog dog in dog_list)
            {
                int points = 0;
                if (File.Exists(testFile))
                {
                    using (ReadTest reader = new ReadTest(testFile))
                    {
                        List<Test> list = new List<Test>();
                        List<int> poi = new List<int>();
                        int counter = 0;

                        list = reader.ReadTestRow();

                        foreach (Test test in list)
                        {
                            if (test.Doggi.VirName == dog.VirName)
                            {
                                poi.Add(test.KisaPoints);
                                counter++;
                            }
                        }

                        if (counter == 0)
                            points = 0;
                        else if (counter > 5)
                        {
                            //Sum 5 highest points
                            int[] array = new int[5];
                            int i = 0, min = 0, minIndex = 0;

                            foreach (int p in poi)
                            {
                                if (i < 5)
                                {
                                    //first 8 values
                                    array[i] = p;
                                }
                                else
                                {
                                    //check if next value is higher than the smallest in the array
                                    //if so overwrite the new value
                                    min = array.Min();
                                    if (p > min)
                                    {
                                        minIndex = Array.IndexOf(array, array.Min());
                                        array[minIndex] = p;
                                    }
                                }
                                i++;
                            }
                            points = array.Sum();
                            //return points;
                        }
                        else
                        {
                            //Sum all points
                            points = poi.Sum();
                            //return points;
                        }
                    }
                }
                dog.fullPointsTest = points + dog.BasePoints;
            }
        }

        public void countShowPoints(string showFile)
        {
            // Points for Kultadoggi competition only for chosen dog. 
            foreach (Dog dog in dog_list)
            {
                int points = 0;
                if (File.Exists(showFile))
                {
                    using (ReadShow reader = new ReadShow(showFile))
                    {
                        List<Show> list = new List<Show>();
                        List<int> poi = new List<int>();
                        int counter = 0;

                        list = reader.ReadShowRow();

                        foreach (Show show in list)
                        {
                            if (show.Doggi.VirName == dog.VirName)
                            {
                                poi.Add(show.Points);
                                counter++;
                            }
                        }

                        if (counter == 0)
                            points = 0;
                        else if (counter > 8)
                        {
                            //Sum 8 highest points
                            int[] array = new int[8];
                            int i = 0, min = 0, minIndex = 0;

                            foreach (int p in poi)
                            {
                                if (i < 8)
                                {
                                    //first 8 values
                                    array[i] = p;
                                }
                                else
                                {
                                    //check if next value is higher than the smallest in the array
                                    //if so overwrite the new value
                                    min = array.Min();
                                    if (p > min)
                                    {
                                        minIndex = Array.IndexOf(array, array.Min());
                                        array[minIndex] = p;
                                    }
                                }
                                i++;
                            }
                            points = array.Sum();
                            //return points;
                        }
                        else
                        {
                            //Sum all points
                            points = poi.Sum();
                            //return points;
                        }
                    }
                }
                dog.fullPointsShow = points;
            }
        }        

        public List<Dog> sorting(string type)
        {
            if (dog_list.Count > 1)
            {
                List<Dog> sort_list = new List<Dog>();
                Dog[] array = new Dog[dog_list.Count];
                int i = 0, point1 = 0, point2 = 0;

                foreach (Dog dog in dog_list)
                {
                    if (i == 0)
                        array[i] = dog;
                    else
                    {
                        for (int j = 0; j < i; j++)
                        {

                            Dog dog2 = new Dog();
                            dog2 = array[j];
                            if (type == "show")
                            {
                                point1=dog2.fullPointsShow;
                                point2 = dog.fullPointsShow;
                            }
                            else if (type == "test")
                            {
                                point1 = dog2.fullPointsTest;
                                point2 = dog.fullPointsTest;
                            }

                            if (point1 < point2)
                            {
                                //vaihda järjestys
                                for (int k = i; k > j; k--)
                                    array[k] = array[k - 1];
                                array[j] = dog;
                                j = i;
                            }
                            else
                            {
                                //lisää loppuun
                                array[i] = dog;
                            }
                        }
                    }
                    i++;
                }
                for (int j = 0; j < dog_list.Count; j++)
                {
                    Dog dog2 = new Dog();
                    dog2 = array[j];
                    sort_list.Add(dog2);
                }
                return sort_list;
            }
            else
            {
                return dog_list;
            }
        }
    }
}
