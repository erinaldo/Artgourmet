   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa;    5Artebit.Restaurante.Caixa.Relatorio.FechamentoPeriodo   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      m iTelerik.Windows.Controls.Docking, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      .��5      �   � RowDefinitions  5	      �� 5
      $	��110 �6   �� 5      $��* �6   .+�5      �� $��0��6    �� CornerRadius$ 3��6   
�������
�������$��1q�6O    J� Opacity$	 0.9Q�6c    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$
 Gray��6'      Opacity$ 1Q�5         ShadowDepth$ 6Q�5      ._�5      � $	��Top=�6
   $��109.96��6"   .+�5      �� $
��Left=�6   $��17��6+   $��	39,22,0,0q�67   $
��Wrap=�6J   $	��Top=�6^   $��79��6v   $��Arial)�6�   $��14(�6�   $
��Bold"�6�   �� 6�    � Language$ pt-br�6�   $�Data Inicial:��6�   6�   �� 5      $
��Left=�6   $��	39,0,0,30q�6+   $
��Wrap=�6>   $��79��6R   $��14.96��6]   $��Bottom=�6l   �� 6�   $ pt-br�6�   $�Data Final:��6�   $��Arial)�6�   $��14(�6�   $
��Bold"�6�   6�   �� 5      -   5         ��Name$ btBuscar��5      $��Buscar��6   $
��Left=�60   $��24��6K   $��319,16.96,0,0q�6W   $	��Top=�6n   $	��100��6�   ���#B68944���#B689446�   �� 5      -   5        $ 
btImprimir��5      $��Imprimir��6!   $
��Left=�64   $��319,53,0,31q�6O   $	��100��6d   ���#B68944���#B689446�   (  "System.Windows.Controls.DatePicker  5      -   5        	  Name$	 	dtInicial��5      $
��Left=�6$   $��
128,16,0,0q�6?   $	��Top=�6S   / 
 )Telerik.Windows.Controls.RadMaskedTextBox  5      -   5        
  Name$
 	txtHorIni��5   +   $
��Left=�6>      Culture$ pt-BR��6`      MaskType$ DateTime=�6p      Mask$ HH:mm��6�   $
��Left=�5   +   $��268,15,0,69.96q�6F   $��45��6^   $��25��6i     5      -   5        $	 dtFinal��5      $
��Left=�6"   $��129,0,0,28.96q�6=   $��Bottom=�6T     5      -   5        $
 	txtHorFim��5   +   $
��Left=�6>   $ pt-BR��6`   $ DateTime=�6p   $ HH:mm��6�   $
��Left=�5   +   $��
268,30,0,0q�6F   $��45��6Z   $��25��6e   5      5       �� 5!      $��1��6   $ 3��6   
�������
�������$��1q�6O   $	 0.9Q�6c    5"        5#      $
 Gray��6'   $ 1Q�5$      $ 6Q�5%      ._�5'      �� $	��510��6   .��5(      � .+�5)      �� -   5         ��Name$ txtRelatorio��5)      $
��Left=�6'   $��	10,10,0,0q�6B   $
��Wrap=�6U   $	��Top=�6i   5+      5,      5-      5.      5/      