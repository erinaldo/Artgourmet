   M S B A M L   `   `   `  ���� T  PArtebit.Restaurante.Caixa, Version=1.0.1.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:Artebit.Restaurante.Caixa.Controles#Artebit.Restaurante.Caixa.Controles  4    .Artebit.Restaurante.Caixa.Cadastro.Impressoras   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    Q	Controles1clr-namespace:Artebit.Restaurante.Caixa.Controles	     	 
    .���� .+�� .+�4   .Artebit.Restaurante.Caixa.Controles.Cabecalho1  -      Name$  	Cabecalho��   Titulo$ Cadastro / Impressoras��   BotaoInicioEvento . � $	��630��$	��630��$��15 0 0 0q� � 	Resources    Headerstyle �� 
TargetType9  3Telerik.Windows.Controls.GridView.GridViewHeaderRow �� Property �� Value �� 
TargetType ��Name	 � ColumnDefinitions��%p  &	        �� "  .���  � ��$ 25����  � �� �� "  .R��� $ borda1�����#B68944���#49181A$��0 0 0 0q�._�� 	 �� $��* ��� $��* ��� $��* ��� $��60 �.+��� ���#49181A$��1q�$��0��._��� $��0 0 0 0q�
�������$��Center=�$��11(�$��Descrição���� ���#49181A$��1q�$��1��._��� $��Center=�
�������$��Center=�$��11(�$��Ligado a���� ���#49181A$��1q�$��2��._��� $��Center=�
�������$��Center=�$��11(�$��Modelo���� ���#49181A$��1q�$��3��._��� $��Center=�
�������$��Center=�$��11(�$��Ativo��
 � RowDefinitions
 �� $��35 ��� $	��43* ��� $
��492* �	 �� $	��10* �.+��� -   $��	Adicionar��$��0��$��1��$	��100��$��27��$
��Left=�$��5,10,0,0q�$	��Top=�  BotaoMenor2#��[ ���� .R��� .+��� $��-40 -10 0 0q�.+���  �� Left$ 0��
 �� Top$ 0��$3��-pack://application:,,,/Img/adicionarItens.png��$��20��$��20��$
��Fill=�$
��Left=��� $��	Adicionar��$
��Bold"�$��12(�$��
-10 -9 0 0q��� -   $��Editar��$��0��$��1��$	��100��$��27��$
��Left=�$	��Top=�$��115,10,0,11q�#��[ ���� .R��� .+��� $��-40 -10 0 0q�.+��� $ 0��$ 0��$+��%pack://application:,,,/Img/editar.png��$��20��$��20��$
��Fill=�$
��Left=��� $��Editar��$
��Bold"�$��12(�$��
-10 -9 0 0q�*  $Telerik.Windows.Controls.RadGridView  -     Name$ 	gridDados��$	��520��$	��520��$	��980��$��0��$��2�� J� Opacity$	 0.9Q�;  5Telerik.Windows.Controls.GridView.GridViewDataControl   CanUserFreezeColumns .     RowIndicatorVisibility$ 	Collapsed=�   AutoGenerateColumns .     
IsReadOnly . $	��Top=�   ShowGroupPanel .     HeaderRowStyle# [  * 	 $Telerik.Windows.Controls.DataControl   ItemsSource �� *AIMPRESSORA+   Columns 1  +Telerik.Windows.Controls.GridViewDataColumn  6  0Telerik.Windows.Controls.GridViewBoundColumnBase   Header$ Descrição��-  'Telerik.Windows.Controls.GridViewColumn   Width?	  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$ *	    IsGroupable .    IsFilterable .     DataMemberBinding �� *	descricao+  $ Ligado a��$ *	  .   .   �� *	ligadoa+  $ Modelo��$ *	    
IsReadOnly .   .   .   �� *modelo+  $ Ativo��$ 60	  .   .   �� *ativo+