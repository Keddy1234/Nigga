using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Reflection;

namespace Hotel.WindowsForms.Servicios;

/// <summary>
/// Paisajes rústicos embebidos exclusivamente para la interfaz administrativa.
/// </summary>
public enum TipoPaisajeAdmin
{
    PrincipalHero,
    SidebarBosque,
    TerrazaModulos,
    RestauranteRustico
}

/// <summary>
/// Centraliza la carga de paisajes rústicos y el estilizado visual de los formularios administrativos.
/// </summary>
public static class TemaAdminRustico
{
    private static readonly object _sync = new();
    private static readonly Dictionary<TipoPaisajeAdmin, Image?> _cache = new();

    private static string ObtenerNombreArchivo(TipoPaisajeAdmin tipo) => tipo switch
    {
        TipoPaisajeAdmin.PrincipalHero => "banner_posada_principal.jpg",
        TipoPaisajeAdmin.SidebarBosque => "paisaje_sidebar.jpg",
        TipoPaisajeAdmin.TerrazaModulos => "paisaje_terraza_rustica.jpg",
        TipoPaisajeAdmin.RestauranteRustico => "paisaje_restaurante_rustico.jpg",
        _ => "paisaje_terraza_rustica.jpg"
    };

    /// <summary>
    /// Obtiene una copia cacheada del paisaje solicitado desde los recursos incrustados del ensamblado.
    /// </summary>
    public static Image? ObtenerImagen(TipoPaisajeAdmin tipo)
    {
        lock (_sync)
        {
            if (_cache.TryGetValue(tipo, out var existente))
                return existente;

            string archivo = ObtenerNombreArchivo(tipo);
            Image? resultado = null;

            try
            {
                var ensamblado = Assembly.GetExecutingAssembly();
                string nombreRecurso = $"Hotel.WindowsForms.Recursos.{archivo}";
                using Stream? stream = ensamblado.GetManifestResourceStream(nombreRecurso);
                if (stream != null)
                {
                    using var temp = Image.FromStream(stream);
                    resultado = new Bitmap(temp);
                }
                else
                {
                    string rutaDisco = Path.Combine(AppContext.BaseDirectory, "Recursos", archivo);
                    if (File.Exists(rutaDisco))
                    {
                        using var temp = Image.FromFile(rutaDisco);
                        resultado = new Bitmap(temp);
                    }
                }
            }
            catch
            {
                resultado = null;
            }

            _cache[tipo] = resultado;
            return resultado;
        }
    }

    /// <summary>
    /// Inserta un encabezado panorámico con paisaje rústico y aplica el tema en los controles del subformulario.
    /// </summary>
    public static void AplicarEsteticaSubFormulario(
        Form form,
        string titulo,
        string subtitulo,
        TipoPaisajeAdmin paisaje = TipoPaisajeAdmin.TerrazaModulos)
    {
        const int AlturaBanner = 84;

        form.SuspendLayout();

        foreach (Control c in form.Controls)
        {
            c.Top += AlturaBanner;
        }

        form.ClientSize = new Size(form.ClientSize.Width, form.ClientSize.Height + AlturaBanner);
        form.BackColor = Color.FromArgb(240, 226, 200);

        var banner = new PanelPaisajeRustico
        {
            Dock = DockStyle.Top,
            Height = AlturaBanner,
            ImagenFondo = ObtenerImagen(paisaje),
            Eyebrow = "POSADA DEL ROBLE  •  ADMINISTRACIÓN",
            Titulo = titulo,
            Subtitulo = subtitulo,
            TextoDerechaSuperior = "Panel Admin",
            EsHeroPrincipal = false,
            SoloVignette = false
        };

        form.Controls.Add(banner);
        EstilizarControlesRecursivo(form);
        form.ResumeLayout(true);
    }

    /// <summary>
    /// Aplica doble búfer, estilos de tabla y realce rústico a los controles hijos de forma recursiva.
    /// </summary>
    public static void EstilizarControlesRecursivo(Control raiz)
    {
        foreach (Control c in raiz.Controls)
        {
            if (c is PanelPaisajeRustico)
                continue;

            if (c is DataGridView or Panel or FlowLayoutPanel)
            {
                HabilitarDobleBufer(c);
            }

            if (c is DataGridView dgv)
            {
                EstilizarDataGridView(dgv);
            }
            else if (c is Button btn && btn.FlatStyle == FlatStyle.Flat)
            {
                btn.Cursor = Cursors.Hand;
                btn.FlatAppearance.MouseOverBackColor = AjustarBrillo(btn.BackColor, 18);
                btn.FlatAppearance.MouseDownBackColor = AjustarBrillo(btn.BackColor, -18);
            }
            else if (c is GroupBox gb)
            {
                gb.BackColor = Color.FromArgb(230, 212, 180);
                gb.ForeColor = Color.FromArgb(46, 31, 20);
            }

            if (c.HasChildren)
            {
                EstilizarControlesRecursivo(c);
            }
        }
    }

    /// <summary>
    /// Configura una paleta cálida de madera y pergamino en una tabla DataGridView.
    /// </summary>
    public static void EstilizarDataGridView(DataGridView dgv)
    {
        dgv.EnableHeadersVisualStyles = false;
        dgv.BorderStyle = BorderStyle.FixedSingle;
        dgv.BackgroundColor = Color.FromArgb(248, 238, 218);
        dgv.GridColor = Color.FromArgb(196, 170, 134);

        dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
        {
            BackColor = Color.FromArgb(56, 36, 22),
            ForeColor = Color.FromArgb(243, 207, 122),
            SelectionBackColor = Color.FromArgb(56, 36, 22),
            SelectionForeColor = Color.FromArgb(243, 207, 122),
            Font = new Font("Georgia", 9.25F, FontStyle.Bold),
            Padding = new Padding(6, 4, 6, 4)
        };
        dgv.ColumnHeadersHeight = 34;

        dgv.DefaultCellStyle = new DataGridViewCellStyle
        {
            BackColor = Color.FromArgb(250, 242, 226),
            ForeColor = Color.FromArgb(46, 31, 20),
            SelectionBackColor = Color.FromArgb(196, 154, 69),
            SelectionForeColor = Color.FromArgb(28, 17, 10),
            Font = new Font("Georgia", 9F),
            Padding = new Padding(4, 2, 4, 2)
        };

        dgv.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
        {
            BackColor = Color.FromArgb(240, 227, 202),
            ForeColor = Color.FromArgb(46, 31, 20),
            SelectionBackColor = Color.FromArgb(196, 154, 69),
            SelectionForeColor = Color.FromArgb(28, 17, 10)
        };

        dgv.RowTemplate.Height = 28;
    }

    /// <summary>
    /// Decora una tarjeta KPI del tablero principal con franja superior de acento y borde dorado sutil.
    /// </summary>
    public static void EstilizarTarjetaKpi(Panel pnl, Color colorAcentoSuperior)
    {
        pnl.BorderStyle = BorderStyle.None;
        HabilitarDobleBufer(pnl);

        pnl.Paint += (_, e) =>
        {
            if (pnl.Width <= 2 || pnl.Height <= 2) return;

            using var brochaAcento = new SolidBrush(colorAcentoSuperior);
            e.Graphics.FillRectangle(brochaAcento, 0, 0, pnl.Width, 4);

            using var plumaBorde = new Pen(Color.FromArgb(165, 130, 74), 1f);
            e.Graphics.DrawRectangle(plumaBorde, 0, 0, pnl.Width - 1, pnl.Height - 1);
        };
    }

    /// <summary>
    /// Indica si el formulario o alguno de los controles indicados ya fue cerrado/dispuesto
    /// (o si un DataGridView perdió sus columnas al disponerse) tras una operación asíncrona.
    /// </summary>
    public static bool EstaCerradoODispuesto(Form form, params Control?[] controles)
    {
        if (form == null || form.IsDisposed || form.Disposing || !form.IsHandleCreated)
            return true;

        foreach (var c in controles)
        {
            if (c == null || c.IsDisposed || c.Disposing)
                return true;

            if (c is DataGridView dgv && dgv.Columns.Count == 0)
                return true;
        }

        return false;
    }

    private static void HabilitarDobleBufer(Control control)
    {
        typeof(Control)
            .GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.SetValue(control, true, null);
    }

    private static Color AjustarBrillo(Color color, int delta)
    {
        int r = Math.Clamp(color.R + delta, 0, 255);
        int g = Math.Clamp(color.G + delta, 0, 255);
        int b = Math.Clamp(color.B + delta, 0, 255);
        return Color.FromArgb(color.A, r, g, b);
    }
}

/// <summary>
/// Panel personalizado con doble búfer para mostrar paisajes rústicos sin parpadeo ni deformación.
/// </summary>
public sealed class PanelPaisajeRustico : Panel
{
    private Image? _imagenFondo;
    private string _eyebrow = string.Empty;
    private string _titulo = string.Empty;
    private string _subtitulo = string.Empty;
    private string _textoDerechaSuperior = string.Empty;
    private string _textoDerechaInferior = string.Empty;
    private bool _esHeroPrincipal;
    private bool _soloVignette;

    public PanelPaisajeRustico()
    {
        SetStyle(
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.UserPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw,
            true);
        DoubleBuffered = true;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Image? ImagenFondo
    {
        get => _imagenFondo;
        set { _imagenFondo = value; Invalidate(); }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string Eyebrow
    {
        get => _eyebrow;
        set { _eyebrow = value; Invalidate(); }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string Titulo
    {
        get => _titulo;
        set { _titulo = value; Invalidate(); }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string Subtitulo
    {
        get => _subtitulo;
        set { _subtitulo = value; Invalidate(); }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string TextoDerechaSuperior
    {
        get => _textoDerechaSuperior;
        set { _textoDerechaSuperior = value; Invalidate(); }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string TextoDerechaInferior
    {
        get => _textoDerechaInferior;
        set { _textoDerechaInferior = value; Invalidate(); }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool EsHeroPrincipal
    {
        get => _esHeroPrincipal;
        set { _esHeroPrincipal = value; Invalidate(); }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool SoloVignette
    {
        get => _soloVignette;
        set { _soloVignette = value; Invalidate(); }
    }

    private static void DibujarImagenCover(Graphics g, Image img, Rectangle destino)
    {
        if (destino.Width <= 0 || destino.Height <= 0 || img.Width <= 0 || img.Height <= 0)
            return;

        float ratioDestino = (float)destino.Width / destino.Height;
        float ratioImagen = (float)img.Width / img.Height;

        float srcX = 0f;
        float srcY = 0f;
        float srcW = img.Width;
        float srcH = img.Height;

        if (ratioImagen > ratioDestino)
        {
            srcW = img.Height * ratioDestino;
            srcX = (img.Width - srcW) / 2f;
        }
        else
        {
            srcH = img.Width / ratioDestino;
            srcY = (img.Height - srcH) / 2f;
        }

        var prevInterp = g.InterpolationMode;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.DrawImage(img, destino, new RectangleF(srcX, srcY, srcW, srcH), GraphicsUnit.Pixel);
        g.InterpolationMode = prevInterp;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (Width <= 0 || Height <= 0) return;

        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

        Rectangle rect = new(0, 0, Width, Height);

        if (_imagenFondo != null)
        {
            DibujarImagenCover(g, _imagenFondo, rect);
        }
        else
        {
            using var fallback = new LinearGradientBrush(
                rect,
                Color.FromArgb(46, 31, 20),
                Color.FromArgb(88, 56, 34),
                LinearGradientMode.Horizontal);
            g.FillRectangle(fallback, rect);
        }

        if (_soloVignette)
        {
            PintarVignetteSidebar(g, rect);
        }
        else
        {
            PintarBannerPanoramico(g, rect);
        }
    }

    private void PintarVignetteSidebar(Graphics g, Rectangle rect)
    {
        int alturaGradiente = Math.Min(64, Height);
        Rectangle rectGrad = new(0, Height - alturaGradiente, Width, alturaGradiente);
        using (var grad = new LinearGradientBrush(
            rectGrad,
            Color.FromArgb(0, 28, 17, 10),
            Color.FromArgb(220, 24, 15, 9),
            LinearGradientMode.Vertical))
        {
            g.FillRectangle(grad, rectGrad);
        }

        using (var plumaMarco = new Pen(Color.FromArgb(196, 154, 69), 2f))
        {
            g.DrawRectangle(plumaMarco, 1, 1, Width - 2, Height - 2);
        }

        using var formatoCentro = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Near,
            Trimming = StringTrimming.EllipsisCharacter
        };

        if (!string.IsNullOrWhiteSpace(_titulo))
        {
            using var fuenteTitulo = new Font("Georgia", 9F, FontStyle.Bold);
            using var brochaSombra = new SolidBrush(Color.FromArgb(190, 15, 9, 5));
            using var brochaTitulo = new SolidBrush(Color.FromArgb(232, 198, 128));
            RectangleF rectTit = new(8, Height - 42, Width - 16, 18);
            g.DrawString(_titulo, fuenteTitulo, brochaSombra, new RectangleF(rectTit.X + 1, rectTit.Y + 1, rectTit.Width, rectTit.Height), formatoCentro);
            g.DrawString(_titulo, fuenteTitulo, brochaTitulo, rectTit, formatoCentro);
        }

        if (!string.IsNullOrWhiteSpace(_subtitulo))
        {
            using var fuenteSub = new Font("Georgia", 7.75F, FontStyle.Italic);
            using var brochaSub = new SolidBrush(Color.FromArgb(245, 236, 220));
            RectangleF rectSub = new(8, Height - 23, Width - 16, 16);
            g.DrawString(_subtitulo, fuenteSub, brochaSub, rectSub, formatoCentro);
        }
    }

    private void PintarBannerPanoramico(Graphics g, Rectangle rect)
    {
        using (var gradHorizontal = new LinearGradientBrush(
            rect,
            Color.FromArgb(215, 28, 17, 10),
            Color.FromArgb(65, 28, 17, 10),
            LinearGradientMode.Horizontal))
        {
            var mezcla = new ColorBlend(3)
            {
                Colors = new[]
                {
                    Color.FromArgb(215, 28, 17, 10),
                    Color.FromArgb(145, 34, 21, 13),
                    Color.FromArgb(65, 28, 17, 10)
                },
                Positions = new[] { 0f, 0.55f, 1f }
            };
            gradHorizontal.InterpolationColors = mezcla;
            g.FillRectangle(gradHorizontal, rect);
        }

        int alturaVignetteInferior = Math.Min(36, Height);
        Rectangle rectVignetteInf = new(0, Height - alturaVignetteInferior, Width, alturaVignetteInferior);
        using (var gradInferior = new LinearGradientBrush(
            rectVignetteInf,
            Color.FromArgb(0, 22, 13, 8),
            Color.FromArgb(120, 22, 13, 8),
            LinearGradientMode.Vertical))
        {
            g.FillRectangle(gradInferior, rectVignetteInf);
        }

        using (var brochaOro = new SolidBrush(Color.FromArgb(196, 154, 69)))
        {
            g.FillRectangle(brochaOro, 0, Height - 3, Width, 3);
        }

        int margenIzq = _esHeroPrincipal ? 26 : 20;
        int anchoTextoMax = Math.Max(220, Width - 260);

        int yCursor = _esHeroPrincipal ? 24 : 11;

        if (!string.IsNullOrWhiteSpace(_eyebrow))
        {
            using var fuenteEyebrow = new Font("Georgia", 8F, FontStyle.Bold);
            using var brochaEyebrow = new SolidBrush(Color.FromArgb(232, 198, 128));
            g.DrawString(_eyebrow, fuenteEyebrow, brochaEyebrow, new RectangleF(margenIzq, yCursor, anchoTextoMax, 16));
            yCursor += _esHeroPrincipal ? 22 : 16;
        }

        if (!string.IsNullOrWhiteSpace(_titulo))
        {
            float tamTitulo = _esHeroPrincipal ? 19.5F : 13.5F;
            using var fuenteTitulo = new Font("Georgia", tamTitulo, FontStyle.Bold);
            using var brochaSombra = new SolidBrush(Color.FromArgb(200, 15, 9, 5));
            using var brochaTitulo = new SolidBrush(Color.FromArgb(250, 242, 226));
            int altoTit = _esHeroPrincipal ? 34 : 25;
            RectangleF rectTit = new(margenIzq, yCursor, anchoTextoMax, altoTit);
            g.DrawString(_titulo, fuenteTitulo, brochaSombra, new RectangleF(rectTit.X + 1, rectTit.Y + 1, rectTit.Width, rectTit.Height));
            g.DrawString(_titulo, fuenteTitulo, brochaTitulo, rectTit);
            yCursor += _esHeroPrincipal ? 36 : 24;
        }

        if (!string.IsNullOrWhiteSpace(_subtitulo))
        {
            float tamSub = _esHeroPrincipal ? 9.5F : 8.75F;
            FontStyle estiloSub = _esHeroPrincipal ? FontStyle.Italic : FontStyle.Regular;
            using var fuenteSub = new Font("Georgia", tamSub, estiloSub);
            using var brochaSub = new SolidBrush(Color.FromArgb(236, 220, 190));
            g.DrawString(_subtitulo, fuenteSub, brochaSub, new RectangleF(margenIzq, yCursor, anchoTextoMax, 22));
        }

        if (!string.IsNullOrWhiteSpace(_textoDerechaSuperior))
        {
            PintarInsigniaDerecha(g);
        }
    }

    private void PintarInsigniaDerecha(Graphics g)
    {
        bool tieneInferior = !string.IsNullOrWhiteSpace(_textoDerechaInferior);
        using var fuenteSup = new Font("Georgia", _esHeroPrincipal ? 8.75F : 8.25F, FontStyle.Bold);
        using var fuenteInf = new Font("Georgia", 10F, FontStyle.Bold);

        SizeF medSup = g.MeasureString(_textoDerechaSuperior, fuenteSup);
        SizeF medInf = tieneInferior ? g.MeasureString(_textoDerechaInferior, fuenteInf) : SizeF.Empty;

        int anchoBadge = Math.Max(110, (int)Math.Ceiling(Math.Max(medSup.Width, medInf.Width)) + 26);
        int altoBadge = tieneInferior ? 48 : 28;
        int xBadge = Math.Max(12, Width - anchoBadge - 22);
        int yBadge = _esHeroPrincipal ? 18 : Math.Max(10, (Height - 3 - altoBadge) / 2);

        Rectangle rectBadge = new(xBadge, yBadge, anchoBadge, altoBadge);
        using (var brochaFondo = new SolidBrush(Color.FromArgb(175, 26, 16, 9)))
        {
            g.FillRectangle(brochaFondo, rectBadge);
        }
        using (var plumaOro = new Pen(Color.FromArgb(196, 154, 69), 1f))
        {
            g.DrawRectangle(plumaOro, rectBadge.X, rectBadge.Y, rectBadge.Width - 1, rectBadge.Height - 1);
        }

        using var formato = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        if (tieneInferior)
        {
            using var brochaCrema = new SolidBrush(Color.FromArgb(245, 232, 208));
            using var brochaOroTexto = new SolidBrush(Color.FromArgb(232, 198, 128));
            g.DrawString(_textoDerechaSuperior, fuenteSup, brochaCrema, new RectangleF(xBadge, yBadge + 4, anchoBadge, 20), formato);
            g.DrawString(_textoDerechaInferior, fuenteInf, brochaOroTexto, new RectangleF(xBadge, yBadge + 22, anchoBadge, 22), formato);
        }
        else
        {
            using var brochaOroTexto = new SolidBrush(Color.FromArgb(232, 198, 128));
            g.DrawString(_textoDerechaSuperior, fuenteSup, brochaOroTexto, new RectangleF(xBadge, yBadge, anchoBadge, altoBadge), formato);
        }
    }
}
