using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;

namespace RestaurantSystem
{
    public partial class Form1 : MetroForm
    {
        private int i = 0; // I need Global Index 

        //list for All purchased Meals
        private List<Meal> PurchasedMeals = new List<Meal>();

        private Meal ChooseMeal;
        private Meal predefinedMeal;
        private bool AdminClicked;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //metroTabPage3.Visible = false;
            metroTabControl1.Controls.Remove(metroTabPage3);

        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            predefinedMeal = MealBuilder.PrepareChiekenMeal();
            ;
            listView1.Items.Clear();
            predefinedMeal.ShowItems(listView1);
            metroLabel1.Text = string.Format("Cost: {0}$", predefinedMeal.GetCost());
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            predefinedMeal = MealBuilder.PrepareBeefMeal();
            listView1.Items.Clear();
            predefinedMeal.ShowItems(listView1);
            metroLabel1.Text = string.Format("Cost: {0}$", predefinedMeal.GetCost());
        }

        private void metroTile3_Click(object sender, EventArgs e)
        {
            predefinedMeal = MealBuilder.PrepareVeggieMeal();
            listView1.Items.Clear();
            predefinedMeal.ShowItems(listView1);
            metroLabel1.Text = string.Format("Cost: {0}$", predefinedMeal.GetCost());
        }

        private void metroTileSandwich_Click(object sender, EventArgs e)
        {
            if (ChooseMeal == null) ChooseMeal = new Meal();
            Sandwiches sandwiche = null;
            Button btn = sender as MetroTile;
            if (btn.Text == @"&Chicken Burgar")
                sandwiche = new ChickenBurger();
            else if (btn.Text == @"&Veggie Burgar")
                sandwiche = new VeggieBurger();
            else if (btn.Text == @"&Beef
Burger")
                sandwiche = new BeefBurger();

            if (sandwiche != null)
            {
                ChooseMeal.AddItem(sandwiche);
                listView2.Items.Clear();
                ChooseMeal.ShowItems(listView2);
            }
            metroLabel2.Text = string.Format("Cost: {0}$", ChooseMeal.GetCost());

        }

        private void metroTile13HotDrinks_Click(object sender, EventArgs e)
        {
            if (ChooseMeal == null) ChooseMeal = new Meal();
            HotDrinks hotDrink = null;
            Button btn = sender as MetroTile;
            if (btn.Text == "&Tea")
                hotDrink = new Tea();
            else if (btn.Text == "&Coffee")
                hotDrink = new Coffee();
            else if (btn.Text == "&Hot chocaholic")
                hotDrink = new HotChocolate();

            if (hotDrink != null)
            {
                ChooseMeal.AddItem(hotDrink);
                listView2.Items.Clear();
                ChooseMeal.ShowItems(listView2);
            }
            metroLabel2.Text = string.Format("Cost: {0}$", ChooseMeal.GetCost());

        }

        private void metroTile8ColdDrink_Click(object sender, EventArgs e)
        {
            if(ChooseMeal==null)ChooseMeal=new Meal();
            ColdDrinks coldDrinks = null;
            Button btn = sender as MetroTile;
            if (btn.Text == "&Pepsi")
                coldDrinks = new Pepsi();
            else if (btn.Text == "&Cola")
                coldDrinks = new Cola();
            else if (btn.Text == "&Juice")
                coldDrinks = new Juice();

            if (coldDrinks != null)
            {
                ChooseMeal.AddItem(coldDrinks);
                listView2.Items.Clear();
                ChooseMeal.ShowItems(listView2);
            }
            metroLabel2.Text = string.Format("Cost: {0}$", ChooseMeal.GetCost());
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView2_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //get the Object clicked
            IItem item = (IItem) e.Item.Tag;

            //remove it From Meal
            ChooseMeal.RemoveItem(item);

            listView2.Items.Clear();
            ChooseMeal.ShowItems(listView2);
            metroLabel2.Text = string.Format("Cost: {0}$", ChooseMeal.GetCost());
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            listView2.View = ChangView();
            listView2.Refresh();
        }

        public View ChangView()
        {
            List<View> viewType = new List<View>() {View.Details, View.LargeIcon, View.SmallIcon, View.List, View.Tile};

            if (i > 4) i = 0;
            return viewType[i++];
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            if (predefinedMeal != null)
            {
                var r = MetroMessageBox.Show(this, "Are You Sure That You Want to Purchase This Meal?",
                    "We need Confarmation Please...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    PurchasedMeals.Add(predefinedMeal);
                    predefinedMeal = null;
                    listView1.Items.Clear();
                }
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (ChooseMeal != null)
                PurchasedMeals.Add(ChooseMeal);
            var r = MetroMessageBox.Show(this, "Are You Sure That You Want to Purchase This Meal?",
                "We need Confarmation Please...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                PurchasedMeals.Add(ChooseMeal);
                ChooseMeal = null;
                listView2.Items.Clear();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (AdminClicked)
            {
                metroTabControl1.Controls.Remove(metroTabPage3);
                AdminClicked = false;
            }
            else
            {
                metroTabControl1.Controls.Add(metroTabPage3);
                AdminClicked = true;
            }
        }

        private void metroTabPage3_Click(object sender, EventArgs e)
        {
            
               
        }

        private void metroContextMenu1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tabPage3.Visible = true;
        }

        private void metroTabPage3_Enter(object sender, EventArgs e)
        {
            listView3.Items.Clear();
            foreach (var purchasedMeal in PurchasedMeals)
            {
                purchasedMeal.ShowItems(listView3);

            }

        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            if (metroTextBox1.Text.Trim() == "admin" && metroTextBox2.Text.Trim() == "123456")
                listView3.Visible = true;
            else
            {
                metroLabel5.Text = "Invaled Login";
            }
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            MetroMessageBox.Show(this, "Basheer ALMOMANI\n Reed Mryyan", "Builder Design Pattern Contributers", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }
    }

    public class Meal
    {
        private readonly List<IItem> _items = new List<IItem>();

        public void AddItem(IItem item)
        {
            _items.Add(item);
        }

        public void RemoveItem(IItem item)
        {
            _items.Remove(item);
        }

        public float GetCost()
        {
            float cost = 0.0f;
            foreach (IItem item in _items)
            {
                cost += item.Price();
            }
            return cost;
        }

        public void ShowItems()
        {
            foreach (var item in _items)
            {
                Console.WriteLine("Item : {0}, Packing : {1}, Price : {2}", item.Name(), item.Packing().Pack(),
                    item.Price());
            }
        }

        internal void ShowItems(ListView listView1)
        {
            foreach (var item in _items)
            {
                var row =
                    new ListViewItem(new[] {item.Name(), item.Packing().Pack(), item.Price().ToString()});
                row.ToolTipText = "Remove This Item";
                row.Tag = item;
                listView1.Items.Add(row);
            }
        }
    }

//preDefined Meals
    public static class MealBuilder
    {

        public static Meal PrepareBeefMeal()
        {
            Meal meal = new Meal();
            meal.AddItem(new BeefBurger());
            meal.AddItem(new Cola());

            return meal;
        }

        public static Meal PrepareChiekenMeal()
        {
            Meal meal = new Meal();
            meal.AddItem(new ChickenBurger());
            meal.AddItem(new Pepsi());

            return meal;
        }

        public static Meal PrepareVeggieMeal()
        {
            Meal meal = new Meal();
            meal.AddItem(new VeggieBurger());
            meal.AddItem(new Juice());

            return meal;
        }



    }

    #region All Objects

    public interface IItem
    {
        string Name();
        IPacking Packing();
        float Price();
    }

    #region Packing Methods

    public interface IPacking
    {
        string Pack();
    }

    public class WrapInPaper : IPacking

    {
        public string Pack()
        {
            return "Wrap In Paper";
        }
    }

    public class WrapInPaperMug : IPacking

    {
        public string Pack()
        {
            return "Wrap In Paper Mug";
        }
    }

    public class WrapInPlasticBottle : IPacking

    {
        public string Pack()
        {
            return "Wrap In Plastic Bottle";
        }
    }

    #endregion

    #region All Sandwiches Classes

    public abstract class Sandwiches : IItem
    {
        public abstract string Name();

        public IPacking Packing()
        {
            return new WrapInPaper();
        }


        public abstract float Price();
    }

    public class ChickenBurger : Sandwiches
    {
        public override string Name()
        {
            return "Chicken Burger";
        }

        public override float Price()
        {
            return 1f;
        }
    }

    public class BeefBurger : Sandwiches
    {
        public override string Name()
        {
            return "Beef Burger";
        }

        public override float Price()
        {
            return 2f;
        }
    }

    public class VeggieBurger : Sandwiches
    {
        public override string Name()
        {
            return "Veggie Burger";
        }

        public override float Price()
        {
            return 3f;
        }
    }

    #endregion

    #region All Hot Drinks Classes

    public abstract class HotDrinks : IItem
    {
        public abstract string Name();


        public IPacking Packing()
        {
            return new WrapInPaperMug();
        }

        public abstract float Price();
    }

    public class Coffee : HotDrinks
    {
        public override string Name()
        {
            return "Coffee";
        }


        public override float Price()
        {
            return .50f;
        }
    }

    public class Tea : HotDrinks
    {
        public override string Name()
        {
            return "Tea";
        }


        public override float Price()
        {
            return .10f;
        }
    }

    public class HotChocolate : HotDrinks
    {
        public override string Name()
        {
            return "Hot Chocolate";
        }


        public override float Price()
        {
            return .45f;
        }
    }

    #endregion

    #region All Cold Drinks Classes

    public abstract class ColdDrinks : IItem
    {
        public abstract string Name();

        public IPacking Packing()
        {
            return new WrapInPlasticBottle();
        }

        public abstract float Price();
    }

    public class Pepsi : ColdDrinks
    {
        public override string Name()
        {
            return "Pepsi";
        }

        public override float Price()
        {
            return .35f;
        }
    }


    public class Cola : ColdDrinks
    {
        public override string Name()
        {
            return "Cola";
        }

        public override float Price()
        {
            return .25f;
        }
    }

    public class Juice : ColdDrinks
    {
        public override string Name()
        {
            return "Juice";
        }

        public override float Price()
        {
            return .50f;
        }
    }
}

#endregion

#endregion
