   M S B A M L   `   `   `  ����   Artebit.Restaurante.Caixa5    /Artebit.Restaurante.Caixa.Cadastro.FormCardapio   m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p	 lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	         	 \
 XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 
   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  
   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006  
   @d2http://schemas.microsoft.com/expression/blend/2008  
   ) 	 #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�( 	 "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�$��	Cardápio��
������ .��� $	��930��
�������.+��� .+�, 	 &Telerik.Windows.Controls.RadTabControl  -   $��tabControl1��$	��530��$��Center=�$	��Top=�$	��900��; 	 5Telerik.Windows.Controls.TabControl.RadTabControlBase   BackgroundVisibility$ 	Collapsed=���Z� .@�) 	 #Telerik.Windows.Controls.RadTabItem  -   $��abaIdentificacao��$��Identificação��$	��110�����#EEEEEE.��� $��3q� � RowDefinitions �� �� �� �� �� �� $��5* ���  � ColumnDefinitions �� $	��99* ��� $
��454* ��� $
��198* �.+��� $��Nome :��$��Center=�$��3q��� $��Ativo :��$��2��$��Center=�$��Normal"�$��3q��� $��Horário Ativo:��$��1��$��Center=�$��3q��� $��
Dia Todo :��$��3��$��Center=�$��Normal"�$��3q��� -   $��imgImagemView��$��2��$��2��$��4��$��2��$��14q��� -    ��Name$ txtNome��$
��Left=�$��1��$	��374��$��25��$	��0,2q��� -   $��ckbAtivo��$��Center=�$
��Left=�$��2��$��1��$��3q�/  )Telerik.Windows.Controls.RadMaskedTextBox  -     Name$ txtHor1��$
��Left=�   Culture$ pt-BR��   MaskType$ DateTime=�	   Mask$	 HH:mm��$
��Left=�$��1��$��1��$��42��$��25��$	��0,2q��� $��à��$��10��$��1��$��1��$��Center=�$
��Left=�$��14(�$��Normal"�$��55 3 3 3q�  -   $ txtHor2��$
��Left=�$ pt-BR��$ DateTime=�$	 HH:mm��$
��Left=�$��1��$��1��$��42��$��25��$��75,0,0,0q��� -   $��
ckbDiaTodo��$��Center=�$
��Left=�$��3��$��1��$��3q�� $��2��$��5��$��
10,0,188,0q� �� �� �� �� �� �� $��* � �� �� $��2* ��� �� $��3* �.+��� $��Dias da semana ativo:��$��0��$��0��$��2��$
��Left=�$��0q��� -	   $��
ckbSegunda��$��Center=�$��Center=�$��1��$��3q��� $��Segunda��$��1��$��Center=�$��1��$��Normal"�$��3q��� -
   $��	ckbQuarta��$��Center=�$��Center=�$��2��$��3q��� $��Quarta��$��1��$��2��$��Center=�$��Normal"�$��3q��� -   $��ckbSexta��$��Center=�$��Center=�$��3��$��3q��� $��Sexta��$��1��$��3��$��Center=�$��Normal"�$��3q��� -   $��
ckbDomingo��$��Center=�$��Center=�$��4��$��3q��� $��Domingo��$��1��$��4��$��Center=�$��Normal"�$��3q��� -   $��ckbTerca��$��Center=�$��Center=�$��1��$��2��$��3q��� $��Terça��$��3��$��1��$��Center=�$��Normal"�$��3q��� -   $��	ckbQuinta��$��Center=�$��Center=�$��2��$��2��$��3q��� $��Quinta��$��3��$��2��$��Center=�$��Normal"�$��3q��� -   $��	ckbSabado��$��Center=�$��Center=�$��3��$��2��$��3q��� $��Sábado��$��3��$��3��$��Center=�$��Normal"�$��3q�  -   $��abaItens��$��Itens��$	��110�����#EEEEEE$	��Top=�.���� .+��� $��4q�
���  ��$��1,1,1,1q�
 �� CornerRadius$
 3��$	��Top=�._��� $	��115��$	��Top=�$��10q�.+�� .+��� $
��Wrap=�$��Cozinha��
���   �$��16(�$��Center=�$��Segoe UI)�$��0 0 10 0q�(  "Telerik.Windows.Controls.RadButton  -   $��btG1Add��$��28��$��32��$
��Left=�$��0,0,2,0q�$	��Top=���Z� ��Z� ���� $(��"pack://application:,,,/Img/add.png��$��20��$��20��$
��Left=�$��1q�  -   $��btG1Edit��$��28��$��32��$
��Left=�$��0,0,2,0q�$	��Top=���Z� ��Z� ���� $+��%pack://application:,,,/Img/editar.png��$��20��$��20��$
��Left=�$��1q�-  'Telerik.Windows.Controls.RadColorPicker  -     Name$ colocPickBG1��   AutomaticColor$
 Blue��   MainPalette$ Metro=�   MainPaletteHeaderText$ Cor de Fundo��  -   $ colocPickFG1��$ Black��$ Metro=�$ Cor da Fonte��  -   $��
btG1Ativar��$��28��$��32��$
��Left=�$��0,0,2,0q�$	��Top=���Z� ��Z� ���� $'��!pack://application:,,,/Img/on.png��$��20��$��20��$
��Left=�$��1q�  -   $��btG1Inativar��$��28��$��32��$
��Left=�$��0,0,2,0q�$	��Top=���Z� ��Z� ���� $(��"pack://application:,,,/Img/off.png��$��20��$��20��$
��Left=�$��1q�+	  %Telerik.Windows.Controls.RadDataPager	  -    	 Name$ pager��$
��Left=�$��0,0,0,0q�$	��Top=� 	  PageSize$ 12�� 	  IsTotalItemCountFixed .   	  DisplayMode$ FirstLastPreviousNext=���Z� �� -    ��Name$ grid_cozinha��$��85������ .R�A�  A� Columns$ 6������ .R�d�  d�Name$ btItem��-   $��2 2 2 2q�$��2q�$��Segoe UI)�$��14(�$	��120��$��35������ *cor+���� *
corFonte+���� *cor+ � DataContext �� ���� *	descricao+�� $��4q�
���  ��$��1,1,1,1q�$
 3��$	��Top=�._��� $	��110��$	��Top=�$��10q�.+�� .+��� $
��Wrap=�$	��Bar��
���   �$��16(�$��Center=�$��Segoe UI)�$��0 0 10 0q�  -   $��btG2Add��$��28��$��32��$
��Left=�$��0,0,2,0q�$	��Top=���Z� ��Z� ���� $(��"pack://application:,,,/Img/add.png��$��20��$��20��$
��Left=�$��1q�  -   $��btG2Edit��$��28��$��32��$
��Left=�$��0,0,2,0q�$	��Top=���Z� ��Z� ���� $+��%pack://application:,,,/Img/editar.png��$��20��$��20��$
��Left=�$��1q�  -   $ colocPickBG2��$
 Blue��$ Metro=�$ Cor de Fundo��  -   $ colocPickFG2��$ Black��$ Metro=�$ Cor da Fonte��  -   $��
btG2Ativar��$��28��$��32��$
��Left=�$��0,0,2,0q�$	��Top=���Z� ��Z� ���� $'��!pack://application:,,,/Img/on.png��$��20��$��20��$
��Left=�$��1q�  -   $��btG2Inativar��$��28��$��32��$
��Left=�$��0,0,2,0q�$	��Top=���Z� ��Z� ���� $(��"pack://application:,,,/Img/off.png��$��20��$��20��$
��Left=�$��1q�	  -    $ pager2��$
��Left=�$��0,0,0,0q�$	��Top=�$ 12�� .  $ FirstLastPreviousNext=���Z� �� -!   $ grid_bar��$��80������ .R�A� $ 6������ .R�d� $ btItem��-"   $��2 2 2 2q�$��Segoe UI)�$��14(�$	��120��$��35������ *cor+���� *
corFonte+���� *cor+ �� ���� *	descricao+�  �� $��* ��� $��* �.+��� $��2q�
���  ��$��1,1,1,1q�$
 3��$	��Top=�._��� $	��190��$	��Top=�$��2q�$��0��.+�� .+��� $
��Wrap=�$
��Cat1��
���   �$��15(�$��Center=�$��Segoe UI)�$��0 0 5 0q�  -#   $��	btCat1Add��$��28��$��27��$
��Left=�$��0,0,2,0q�$	��Top=���Z� ��Z� ���� $(��"pack://application:,,,/Img/add.png��$��20��$��20��$
��Left=�$��1q�  -$   $��
btCat1Edit��$��28��$��27��$
��Left=�$��0,0,2,0q�$	��Top=���Z� ��Z� ���� $+��%pack://application:,,,/Img/editar.png��$��20��$��20��$
��Left=�$��1q�  -%   $ colocPickBCat1��$
 Blue��$ Metro=�$ Cor de Fundo��  -&   $ colocPickFCat1��$ Black��$ Metro=�$ Cor da Fonte��  -'   $��btCat1Ativar��$��28��$��28��$
��Left=�$��0,0,2,0q�$	��Top=���Z� ��Z� ���� $'��!pack://application:,,,/Img/on.png��$��20��$��20��$
��Left=�$��1q�  -(   $��btCat1Inativar��$��28��$��28��$
��Left=�$��0,0,2,0q�$	��Top=���Z� ��Z� ���� $(��"pack://application:,,,/Img/off.png��$��20��$��20��$
��Left=�$��1q�	  -)   $ 	pagerCat1��$
��Left=�$��0,0,0,0q�$	��Top=�$ 12�� .  $ FirstLastPreviousNext=���Z� �� -*   $ 	grid_Cat1��$	��160������ .R�A� $ 3������ .R�d� $ btItem��-+   $��2 2 2 2q�$��Segoe UI)�$��14(�$	��120��$��35������ *cor+���� *
corFonte+���� *cor+ �� ���� *	descricao+�� $��2q�
���  ��$��1,1,1,1q�$
 3��$��1��$	��Top=�._��� $	��190��$	��Top=�$��2q�$��1��.+�� .+��� $
��Wrap=�$
��Cat2��
���   �$��16(�$��Center=�$��Segoe UI)�$��0 0 5 0q�  -,   $��	btCat2Add��$��28��$��27��$
��Left=�$��0,0,2,0q�$	��Top=���Z� ��Z� ���� $(��"pack://application:,,,/Img/add.png��$��20��$��20��$
��Left=�$��1q�  --   $��
btCat2Edit��$��28��$��27��$
��Left=�$��0,0,2,0q�$	��Top=���Z� ��Z� ���� $+��%pack://application:,,,/Img/editar.png��$��20��$��20��$
��Left=�$��1q�  -.   $ colocPickBCat2��$
 Blue��$ Metro=�$ Cor de Fundo��  -/   $ colocPickFCat2��$ Black��$ Metro=�$ Cor da Fonte��  -0   $��btCat2Ativar��$��28��$��27��$
��Left=�$��0,0,2,0q�$	��Top=���Z� ��Z� ���� $'��!pack://application:,,,/Img/on.png��$��20��$��20��$
��Left=�$��1q�  -1   $��btCat2Inativar��$��28��$��27��$
��Left=�$��0,0,2,0q�$	��Top=���Z� ��Z� ���� $(��"pack://application:,,,/Img/off.png��$��20��$��20��$
��Left=�$��1q�	  -2   $ 	pagerCat2��$
��Left=�$��0,0,0,0q�$	��Top=�$ 9�� .  $ FirstLastPreviousNext=���Z� �� -3   $ 	grid_Cat2��$	��160������ .R�A� $ 3������ .R�d� $ btItem��$��2 2 2 2q�$��Segoe UI)�$��14(�$	��120��$��35������ *cor+���� *
corFonte+���� *cor+ �� ���� *	descricao+�� $	��top=�$��1��$��40��._�� .+�  -4   $��	btnSalvar��$��Salvar��$��0 0 100 -10q�$��Right=�$��80��$��25��   BotaoMenor2#��[    -5   $��btnCancelar��$��Cancelar��$��
0 0 10 -10q�$��Right=�$��80��$��25��#��[  