   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa2    ,Artebit.Restaurante.Caixa.Cadastro.GrupoMesa   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      m iTelerik.Windows.Controls.Docking, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5   7   .��5	      � $	��630��6   $	��630��6     � 	Resources  5
   
      Headerstyle �� 
TargetType9  3Telerik.Windows.Controls.GridView.GridViewHeaderRow �� Property �� Value �� 
TargetType ��Name � ColumnDefinitions��%�  &	        �� 5      "  6(   .�5      ��  � ��$ 25��6+   �� 5       � �� 5      �� 5      "  6*   .R�5      �� $ borda1��6%   ���#B68944���#49181A$��0 0 0 0q�5   %   ._�5   "   �  5   &   �� 5   *   $��* �6;   �� 5   *   $��* �6;   �� 5   *   $��* �6;   .+�5   &   �� ���#49181A$��1q�5   -   $��0��5    -   ._�5"   *   �� $��0 0 0 0q�60   
�������$��Center=�5$   4   $��11(�5%   4   $��Descrição��5&   4   5'   '   �� 5)   &   ���#49181A$��1q�5+   -   $��1��5,   -   ._�5.   *   �� $��Center=�60   
�������$��Center=�50   4   $��11(�51   4   $��Quantidade Minima��52   4   53   '   �� 56   &   ���#49181A$��1q�58   -   $��2��59   -   ._�5;   *   �� $��Center=�60   
�������$��Center=�5=   4   $��11(�5>   4   $��Quantidade Maxima��5?   4   5@   '   5B   #   5C      5F      5H      5I      5J       � RowDefinitions 5K   
   �� 5L      $��35 �6   �� 5M      $	��43* �6   �� 5N      $
��492* �6    5P   
   �� 5Q      $	��10* �6   .+�5T   
   �� -   5        $��	Adicionar��5T      $��0��6"   $��1��62   $	��100��6?   $��27��6K   $
��Left=�6W   $��5,10,0,0q�6r   $	��Top=�6�     BotaoMenor2#��[ 6�   ��5U      �� 5V      .R�5W      �� .+�5X      �� $��-40 -10 0 0q�6!   .+�5Y      ��  �� Left$ 0��6%   
	 �� Top$	 0��65   $3��-pack://application:,,,/Img/adicionarItens.png��6D   $��20��6{   $��20��6�   $
��Fill=�6�   $
��Left=�6�   5Z      �� 5[      $��	Adicionar��6$   $
��Bold"�65   $��12(�6G   $��
-10 -9 0 0q�6U   5]      5^      5`      �� 5a   
   -   5        $��Editar��5a      $��0��6   $��1��6/   $	��100��6<   $��27��6H   $
��Left=�6T   $	��Top=�6o   $��115,10,0,11q�6�   #��[ 6�   ��5b      �� 5c      .R�5d      �� .+�5e      �� $��-40 -10 0 0q�6!   .+�5f      �� $ 0��6%   $	 0��65   $+��%pack://application:,,,/Img/editar.png��6D   $��20��6s   $��20��6   $
��Fill=�6�   $
��Left=�6�   5g      �� 5h      $��Editar��6$   $
��Bold"�62   $��12(�6D   $��
-10 -9 0 0q�6R   5i      5j      5l      *  $Telerik.Windows.Controls.RadGridView  5o   
   -   5        
  Name$
 	gridDados��5o      $	��520��6{   $	��520��6�   $	��980��6�   $��0��6�   $��2��6�    J� Opacity$	 0.9Q�6�   ;  5Telerik.Windows.Controls.GridView.GridViewDataControl   CanUserFreezeColumns .     RowIndicatorVisibility$ 	Collapsed=�5p   :      AutoGenerateColumns .     
IsReadOnly . $	��Top=�5q   0      ShowGroupPanel .     HeaderRowStyle# [  6_   * 	 $Telerik.Windows.Controls.DataControl   ItemsSource 5o   X   �� *
RGRUPOMESA+   Columns 5r      1  +Telerik.Windows.Controls.GridViewDataColumn  5s      6  0Telerik.Windows.Controls.GridViewBoundColumnBase   Header$ Descrição��6-   -  'Telerik.Windows.Controls.GridViewColumn   Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$ * 6h      IsGroupable .    IsFilterable .     DataMemberBinding 6@   �� *	descricao+6�     5t      $ Quantidade Minima��6-   $ * 6r    .   .   6H   �� *quantMinima+6�     5u      $ Quantidade Máxima��6-   $ * 6r      
IsReadOnly .   .   .   6H   �� *quantMaxima+6�   5x      5{      5|      