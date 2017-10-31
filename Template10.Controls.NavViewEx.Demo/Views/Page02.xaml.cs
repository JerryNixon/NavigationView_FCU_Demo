using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page02 : Page
    {
        public Page02()
        {
            InitializeComponent();
            ContentTextBlock.Blocks.Add(GenParagraph("Lorem ipsum dolor sit amet, consectetur adipiscing elit.Cras sapien libero, blandit ut porttitor sed, efficitur at sapien. Phasellus ac risus id nunc elementum auctor.Maecenas lobortis dignissim nibh, eget bibendum est fringilla vitae. Curabitur orci tellus, tempus gravida viverra et, sagittis dapibus arcu. Praesent ut turpis diam. Fusce sit amet purus sit amet velit commodo sagittis.Mauris ac orci nibh. Pellentesque fringilla eros ut velit tempor iaculis.Ut massa turpis, pulvinar ac nisl in, placerat elementum magna.Sed est nibh, auctor at est nec, tincidunt elementum augue. Donec varius metus nec ligula tincidunt sollicitudin."));
            ContentTextBlock.Blocks.Add(GenParagraph("In sodales nisl at mi porta rutrum.Suspendisse potenti. Nam scelerisque faucibus eros eget vehicula. Nulla et condimentum nisi, in interdum enim. Nam mauris massa, dignissim ut nisl ut, venenatis accumsan erat. Quisque vel malesuada diam. Suspendisse eu velit felis. Maecenas eget nibh ac elit convallis congue ac at enim. Praesent euismod posuere neque, sed porta neque pellentesque in."));
            ContentTextBlock.Blocks.Add(GenParagraph("In in facilisis risus. Etiam tristique laoreet finibus. Nam egestas ultricies molestie. In ac leo vel mi laoreet semper quis a massa. Donec velit mi, imperdiet sit amet rhoncus at, molestie quis ligula.In fermentum ante odio, a euismod lacus viverra vitae. Donec et orci magna."));
            ContentTextBlock.Blocks.Add(GenParagraph("Fusce magna orci, egestas a elit sit amet, elementum vulputate dui.Integer viverra malesuada viverra. Praesent in dolor et magna accumsan auctor.In tempor, lectus sed ultricies fermentum, massa elit venenatis leo, a consequat sem elit vitae enim.Duis eget velit sit amet mauris sodales aliquet eget sollicitudin nulla.Sed porttitor viverra ultricies. Sed metus purus, pulvinar non ipsum tincidunt, porttitor elementum ex. Curabitur scelerisque lobortis purus, ac consequat nunc venenatis ut. Maecenas egestas sagittis orci ut dictum. Lorem ipsum dolor sit amet, consectetur adipiscing elit.Integer facilisis suscipit tempor. Donec a magna nibh. Fusce at lectus at enim sodales dignissim.Proin magna quam, ullamcorper ut libero laoreet, suscipit egestas ipsum. Aenean convallis tempus enim, ac ultricies tortor consectetur sed."));
            ContentTextBlock.Blocks.Add(GenParagraph("Phasellus sed scelerisque turpis. Suspendisse vel dolor egestas eros euismod semper eget sit amet magna.Nullam eleifend dapibus placerat. Duis maximus massa ut tempor gravida. Mauris et tristique odio. Fusce eleifend purus ac diam suscipit, sed eleifend mi vestibulum.Praesent accumsan ligula diam, vel interdum magna ornare feugiat. Vestibulum vel arcu quam. Donec vel venenatis neque. Ut ornare, lectus quis pretium sollicitudin, ipsum lacus imperdiet diam, fermentum elementum quam tellus quis ligula.Cras magna lacus, luctus ut ex nec, convallis laoreet libero. Cras auctor sapien sem, sit amet accumsan purus convallis ac.Fusce at auctor leo. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.Sed sit amet ex facilisis, posuere magna sit amet, ultrices sapien."));
            ContentTextBlock.Blocks.Add(GenParagraph("Vivamus et fermentum velit. Etiam sed sapien nisl. Interdum et malesuada fames ac ante ipsum primis in faucibus.In dapibus ipsum vel turpis consequat, in vehicula felis porta.Nullam aliquam metus at est scelerisque dignissim.Nunc non ligula sit amet ligula tincidunt pretium scelerisque suscipit quam.Sed placerat nulla id est posuere fringilla eu at ipsum. Fusce non neque eu purus tristique suscipit.Sed a felis fringilla, euismod ipsum sit amet, efficitur ligula."));
            ContentTextBlock.Blocks.Add(GenParagraph("Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Vivamus sed ante mauris. Nam vitae diam sem. Aliquam a tortor turpis. Quisque felis tellus, efficitur ac felis vel, congue congue lacus. Quisque bibendum ultricies lectus, a sagittis arcu hendrerit quis. Sed tempor odio a nisl fermentum, non efficitur neque tempus.Sed pharetra suscipit euismod. Proin sagittis quam nec convallis bibendum. Suspendisse vehicula odio condimentum, cursus nibh ac, congue urna.Aliquam nec quam id eros lacinia auctor.Aliquam egestas lorem vel faucibus sagittis. Sed pulvinar neque et augue posuere finibus.In leo urna, lacinia quis libero in, tristique gravida arcu."));
            ContentTextBlock.Blocks.Add(GenParagraph("Proin feugiat arcu diam, eget consectetur mi porttitor congue. Ut consequat imperdiet turpis quis consequat. In fringilla, felis quis molestie consectetur, nibh ligula ullamcorper felis, et condimentum ligula enim ac massa.Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Praesent ultricies ante quam, vel convallis odio pretium sed. Aliquam tempor nulla nec justo fringilla pulvinar.Ut at gravida tortor. Mauris pellentesque imperdiet pretium. Phasellus ut imperdiet nibh, vel pulvinar quam. Phasellus hendrerit ligula quis nibh pellentesque malesuada."));
            ContentTextBlock.Blocks.Add(GenParagraph("Suspendisse potenti. Integer interdum cursus pharetra. Cras in urna finibus, tempus turpis condimentum, lobortis libero.Nam quis nisi eu mi luctus volutpat ut ac mauris. Aliquam malesuada sit amet orci id vulputate.In rutrum purus non porttitor tempus. Aliquam erat volutpat.Quisque eros neque, venenatis a pretium quis, consequat a lorem."));
            ContentTextBlock.Blocks.Add(GenParagraph("In scelerisque porttitor placerat. Quisque maximus aliquet felis eu pulvinar. Praesent tristique metus sed ante porttitor imperdiet.Suspendisse placerat erat non elit congue suscipit.Cras sollicitudin neque vitae lectus sollicitudin, et varius libero auctor.Sed ut purus quis mauris laoreet suscipit.Nullam mattis maximus nunc a luctus. Aliquam ornare porttitor molestie. Nulla rutrum nec dui tincidunt porta. Nunc in dui diam. Maecenas lacus dolor, elementum et rutrum non, interdum quis enim. Praesent viverra tortor tortor, eget vehicula sem elementum at. Aenean odio orci, hendrerit ac vulputate ut, ultricies vitae lacus."));
            ContentTextBlock.Blocks.Add(GenParagraph("Ut finibus ac nisl ac sagittis. Praesent mollis turpis sit amet fringilla lacinia.Morbi dignissim tortor odio, a commodo nibh facilisis nec. Cras vel libero erat. Aenean venenatis mattis arcu eu pellentesque. Pellentesque quis risus in justo facilisis scelerisque.Ut aliquet nunc vitae molestie ullamcorper. Praesent nec libero eu enim auctor malesuada.Curabitur dignissim finibus facilisis. Integer a consequat sapien."));
            ContentTextBlock.Blocks.Add(GenParagraph("Nulla mauris dolor, porttitor vestibulum nisi et, mattis mollis velit. Fusce sit amet tortor ac sem sodales egestas sed ac magna.Duis sit amet accumsan mauris.Fusce non nisi interdum, pellentesque diam aliquam, euismod tortor.In id vestibulum mauris, nec sagittis mauris. Aliquam erat volutpat.Praesent ut tellus placerat, maximus mi id, scelerisque sem.Praesent vehicula sagittis tincidunt. Phasellus egestas pretium quam ac pulvinar."));
        }

        Paragraph GenParagraph(string text)
        {
            var paragraph = new Paragraph { Margin = new Thickness(0, 0, 0, 16) };
            paragraph.Inlines.Add(new Run { Text = text });
            return paragraph;
        }
    }
}
