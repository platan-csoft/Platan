using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace DoubleBufferedDGV
{
    /// <summary>
    ///  Унаследованый от DataGridView класс, 
    ///  в котором просто получаем доступ к protected свойству 
    /// </summary>
    [Description("DataGridView с установленым свойством DoubleBuffered = true")]
    public class CustomDataGridView : DataGridView
    {
        public CustomDataGridView()
        {
            // и устанавливаем значение true при создании экземпляра класса
            this.DoubleBuffered = true;

            // или с помощью метода SetStyle
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            this.UpdateStyles();
        }
    }
}
