   M S B A M L   `   `   `  ����   Artebit.Restaurante.Caixa;    5Artebit.Restaurante.Caixa.Relatorio.FechamentoPeriodo   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    .���   � RowDefinitions  �� $	��110 ��� $��* �.+��� $��0�� �� CornerRadius$ 3��
�������
�������$��1q� J� Opacity$	 0.9Q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$
 Gray��   Opacity$ 1Q�   ShadowDepth$ 6Q�._�� $	��Top=�$��109.96��.+��� $
��Left=�$��17��$��	39,22,0,0q�$
��Wrap=�$	��Top=�$��79��$��Arial)�$��14(�$
��Bold"���  � Language$ pt-br�$�Data Inicial:���� $
��Left=�$��	39,0,0,30q�$
��Wrap=�$��79��$��14.96��$��Bottom=��� $ pt-br�$�Data Final:��$��Arial)�$��14(�$
��Bold"��� -    ��Name$ btBuscar��$��Buscar��$
��Left=�$��24��$��319,16.96,0,0q�$	��Top=�$	��100�����#B68944���#B68944�� -   $ 
btImprimir��$��Imprimir��$
��Left=�$��319,53,0,31q�$	��100�����#B68944���#B68944(  "System.Windows.Controls.DatePicker  -   	  Name$	 	dtInicial��$
��Left=�$��
128,16,0,0q�$	��Top=�/ 
 )Telerik.Windows.Controls.RadMaskedTextBox  -   
  Name$
 	txtHorIni��$
��Left=�   Culture$ pt-BR��   MaskType$ DateTime=�   Mask$ HH:mm��$
��Left=�$��268,15,0,69.96q�$��45��$��25��  -   $	 dtFinal��$
��Left=�$��129,0,0,28.96q�$��Bottom=�  -   $
 	txtHorFim��$
��Left=�$ pt-BR��$ DateTime=�$ HH:mm��$
��Left=�$��
268,30,0,0q�$��45��$��25���� $��1��$ 3��
�������
�������$��1q�$	 0.9Q�   $
 Gray��$ 1Q�$ 6Q�._��� $	��510��.��� .+��� -    ��Name$ txtRelatorio��$
��Left=�$��	10,10,0,0q�$
��Wrap=�$	��Top=�