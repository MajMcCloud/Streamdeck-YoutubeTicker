using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeTicker
{
    internal class UnusedCode
    {


        //public static void CheckInternet()
        //{
        //    int i = 0;

        //    tmInternet = new System.Timers.Timer(10000);
        //    tmInternet.Elapsed += (s, en) =>
        //    {
        //        var p = new Ping();
        //        try
        //        {
        //            var res = p.Send("1.1.1.1", 1000);
        //            if (res.Status != IPStatus.Success)
        //            {
        //                i++;
        //                return;
        //            }

        //            tmInternet.Stop();

        //            if (i > 0)
        //            {
        //                Timer_Elapsed(null, null);
        //            }

        //        }
        //        catch
        //        {
        //            return;
        //        }
        //    };

        //    tmInternet.Start();
        //}

        //private static void NetworkChange_NetworkAvailabilityChanged(object sender, System.Net.NetworkInformation.NetworkAvailabilityEventArgs e)
        //{
        //    if (!e.IsAvailable)
        //        return;

        //    if (Loading)
        //        return;

        //    Timer_Elapsed(null, null);
        //}

        //public static System.Drawing.Image GetImageFromManifest(string sPath)
        //{
        //    // Ready the return
        //    System.Drawing.Image oImage = null;

        //    try
        //    {
        //        // Get the assembly
        //        Assembly oAssembly = Assembly.GetExecutingAssembly();

        //        string[] names = oAssembly.GetManifestResourceNames();

        //        System.Resources.ResourceManager RM = new System.Resources.ResourceManager("YoutubeTicker.Properties.Resources", typeof(Resources).Assembly);

        //        oImage = (Image)RM.GetObject(sPath);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Missing image?           
        //    }

        //    //Return the image
        //    return oImage;
        //}



        ////Kein letztes Video verfügbar. Kanallogo setzen
        //if (lastvid?.url == null)
        //{
        //    //Bitmap bmp2 = entry.GetChannelIcon();

        //    //if (image != null && bmp2 == null)
        //    //{
        //    //    String avatar = image.GetAttributeValue("src", "");

        //    //    if (avatar == "")
        //    //        return;

        //    //    bmp2 = Model.Youtube.Tools.DownloadFromUrl(avatar);
        //    //}
        //    //else if (bmp2 == null)
        //    //{
        //    //    image = doc.DocumentNode.SelectSingleNode("//link[@rel='image_src']");

        //    //    String avatar = image.GetAttributeValue("href", "");

        //    //    if (avatar == "")
        //    //        return;

        //    //    bmp2 = Model.Youtube.Tools.DownloadFromUrl(avatar);
        //    //}

        //    //if (bmp2.Width > 100)
        //    //{
        //    //    bmp2 = ResizeImage(bmp2, 100, 100);
        //    //}

        //    //if (!File.Exists(entry.ChannelImageFile))
        //    //{
        //    //    bmp2.Save(entry.ChannelImageFile);
        //    //}

        //    //VideoCache.Add(entry);

        //    await Render(Context);

        //    return;
        //}




        //private void Render()
        //{
            //Bitmap bmp = bmp_channel_icon.Clone() as Bitmap;



            //using (Graphics g = Graphics.FromImage(bmp))
            //{
            //    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            //    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            //    switch (entry.IconType)
            //    {
            //        case "thumbnail":

            //            if (entry.PreviewType != "live" && entry.LoadThumbnailIconFromDisk() != null) // settings[Context]["preview"].ToString()
            //            {
            //                g.FillRectangle(Brushes.Black, 0, 0, 100, 100);
            //                g.DrawImage(entry.LoadThumbnailIconFromDisk(), 0, 0, bmp.Width, bmp.Height - 26);
            //            }
            //            else if (entry.PreviewType == "live")
            //            {
            //                g.FillRectangle(Brushes.Black, 0, 0, 100, 100);
            //                var ti = bmp_channel_icon;

            //                if (!entry.IsLive)
            //                {
            //                    ti = ImageTools.MakeGrayscale3(ti);
            //                }

            //                g.DrawImage(ti, 0, 0, bmp.Width, bmp.Height);
            //            }

            //            break;

            //        case "channel":

            //            if (entry.PreviewType == "live")
            //            {
            //                g.FillRectangle(Brushes.Black, 0, 0, 100, 100);
            //                var ti = bmp_channel_icon;

            //                if (!entry.IsLive)
            //                {
            //                    ti = ImageTools.MakeGrayscale3(ti);
            //                }

            //                g.DrawImage(ti, 0, 0, bmp.Width, bmp.Height);
            //            }

            //            break;

            //        default:

            //            break;

            //    }

            //    //Blur bottom part of Background
            //    if (entry.ShouldRenderLengthPreview())
            //    {
            //        AnyBitmap f = ImageTools.Blur(bmp, 5);

            //        g.DrawImage(f, new System.Drawing.RectangleF(0, bmp.Height - 26, bmp.Width, 26), new System.Drawing.RectangleF(0, bmp.Height - 26, bmp.Width, 26), GraphicsUnit.Pixel);

            //        var layer = System.Drawing.Color.FromArgb(50, 80, 80, 80);

            //        if (!ImageTools.isDark(bmp))
            //        {
            //            layer = System.Drawing.Color.FromArgb(190, 80, 80, 80);
            //        }

            //        var br = new Pen(layer);
            //        g.FillRectangle(br.Brush, 0, bmp.Height - 26, bmp.Width, 26);

            //        AnyBitmap b = null;

            //        if (entry.VideoLength == "LIVE")
            //        {
            //            b = Properties.Resources.hot;
            //        }
            //        else if ((entry.LastVideo?.ToString() ?? "") != (entry.VideoId ?? ""))
            //        {
            //            if (DateTime.Now.Subtract(entry.VideoSeen ?? entry.LastUpdate).TotalMinutes < 60)
            //            {
            //                b = Resources.hot;
            //            }
            //            else
            //            {
            //                b = Resources._new;
            //            }
            //        }
            //        else
            //        {
            //            b = Properties.Resources.played;
            //        }

            //        g.DrawImage(b, bmp.Width - (b.Width / 2) - 10, bmp.Height - (b.Height / 2) - 5, b.Width / 2, b.Height / 2);

            //        FontFamily ff = new FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif);

            //        StringFormat sf = new StringFormat();

            //        sf.Alignment = StringAlignment.Far;

            //        g.DrawString(entry.VideoLength, new System.Drawing.Font(ff, 14, System.Drawing.FontStyle.Regular, GraphicsUnit.Pixel), Brushes.White, new System.Drawing.RectangleF(2, bmp.Height - 22, bmp.Width - (b.Width / 2) - 15, 26), sf);

            //    }
            //}

        //}





    }
}
