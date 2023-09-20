using physics_equations;
using System.Windows.Forms;
using System.Linq;

namespace math_application
{
    public partial class Form1 : Form
    {
        PhysicsEquations physics = new PhysicsEquations();
        List<string> suggestions = new List<string>();

        myChart chart;
        public Form1()
        {
            InitializeComponent();
            chart = myChart1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.AutoCompleteCustomSource.AddRange(physics.getAllMethods().Split("\n"));
            suggestions.AddRange(physics.getAllMethods().Split("\n"));
            suggestions.AddRange(physics.getAllVariables().Split("\n"));
            textBox1.AutoCompleteCustomSource.AddRange(physics.getAllVariables().Split("\n"));
            if (suggestions.Count > 0)
            {
                listBox1.Enabled = true;
                listBox1.Visible = true;

                listBox1.DataSource = suggestions;
            }
            else if (suggestions.Count > 8)
            {
                listBox1.Enabled = false;
                listBox1.Visible = false;
            }
            else
            {
                listBox1.Enabled = false;
                listBox1.Visible = false;
            }

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
            {
                string s = physics.calculateMultiple(textBox1.Text);
                label1.Text = s;
            }
            if (suggestions.Count >= 1 && e.KeyCode == Keys.Tab)
            {
                e.SuppressKeyPress = true;
                ApplySuggestion(suggestions[0]);
            }
            else if (suggestions.Count >= 8)
            {
                e.SuppressKeyPress = false;
            }
        }
        private string GetCurrentWord()
        {
            string text = textBox1.Text;

            int position = textBox1.SelectionStart;

            // Keep scanning backwords until a non-word character is found
            while (position > 0 && IsWordCharacter(text[position - 1]))
            {
                position--;
            }

            string currentWord = text.Substring(position,
               textBox1.SelectionStart - position);

            return currentWord;
        }

        private bool IsWordCharacter(char c)
        {
            return char.IsLetterOrDigit(c) || c == '_';
        }
        private void ApplySuggestion(string suggestion)
        {
            string currentWord = GetCurrentWord();

            string start = textBox1.Text.Substring(0,
               textBox1.SelectionStart - currentWord.Length);

            string end = textBox1.Text.Substring(
               textBox1.SelectionStart);

            textBox1.Text = start + suggestion + end;

            textBox1.SelectionStart = textBox1.Text.Length;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Get current word 
            string currentWord = GetCurrentWord();
            if (!string.IsNullOrEmpty(currentWord))
            {
                currentWord = char.ToUpper(currentWord[0]) + currentWord.Substring(1);
            }            // Filter suggestions  
            suggestions = new List<string>();
            foreach (string s in textBox1.AutoCompleteCustomSource)
            {
                if (s.Contains(currentWord))
                {
                    suggestions.Add(s);
                }
            }
            if (suggestions.Count > 0)
            {
                listBox1.Enabled = true;
                listBox1.Visible = true;
                Point clientCursorPos = listBox1.Parent.PointToClient(Cursor.Position);
                clientCursorPos = new Point(clientCursorPos.X,textBox1.Location.Y + textBox1.Size.Height);
                listBox1.Location = clientCursorPos;
                listBox1.DataSource = suggestions;
            }
            else if (suggestions.Count > 8)
            {
                listBox1.Enabled = false;
                listBox1.Visible = false;
            }
            else
            {
                listBox1.Enabled = false;
                listBox1.Visible = false;
            }
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string tmpStr = "";
            tmpStr += listBox1.GetItemText(listBox1.SelectedItem);


            ApplySuggestion(tmpStr);
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            string s = physics.calculateMultiple(textBox1.Text);
            label1.Text = s;
            List<PointF> functionPoints = CalculatePointsOnX(Math.Cos, -100, 100,0.05);
            chart.DataPoints = functionPoints;

            chart.paint();
        }


        private List<PointF> CalculatePointsOnX(Func<double, double> mathFunction, double startX, double endX, double stepSize)
        {
            List<PointF> points = new List<PointF>();

            for (double x = startX; x <= endX; x += stepSize)
            {
                double y = mathFunction(x);
                points.Add(new PointF((float)x, (float)y));
            }

            return points;
        }

    }
}