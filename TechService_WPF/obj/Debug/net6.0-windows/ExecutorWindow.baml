   M S B A M L   `   `   `  ����J  FTechService_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:TechService_WPFTechService_WPF  5   	   $    TechService_WPF.ExecutorWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:TechService_WPF     5   	   -   5          � Title$  Исполнитель��5   	   $	��450��6   $	��800��6*    � WindowStartupLocation$ CenterScreen=�66   .��5	      �  � ColumnDefinitions 5
   
   �� 5      �� 5       � RowDefinitions 5   
   �� 5      $
��0.2* �6   �� 5      �� 5      $
��0.2* �6   �� 5      $
��0.2* �6   .+�5   
   �� -   5        $��L_WindowName��5   �   $��0��6   $��0��6   $��2��6-   $��Заявки��6A   $��12(�6R   $��Center=�6`   $��Center=�6}   6�   �� 5   
   -   5        $��	L_Comment��5   �   $��2��6   $��0��6   $��Комментарий:��6-   $��12(�6D   $��Center=�6R   $��Center=�6o   6�   �� 5   
   -   5        $��
TB_Comment��5   �   $��2��6   $��1��6   
���   �$��Center=�6C   $
��Left=�6e   $��12(�6�   6�   �� 5   
   -   5        $��B_Logout��5   u   $��0��6   $��1��6   $��
Выйти��6.   $��Right=�6>   $��Center=�6Z   6�   �� 5   
   -   5        $��B_AddComment��5   �   $��3��6   $��0��6   $��2��6.   $-��'Добавить комментарий��6B   $��Center=�6a   $��Center=�6~   $��	20 0 20 0q�6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��DG_Requests��5   c   $��1��6   $��0��6       CanUserAddRows$ False��60      AutoGenerateColumns$ False��6G      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Номер заявки��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *NumberRequest+  5       $# Дата добавления��6%    6>   �� *DateAddingRequest+  5!      $0 *Дата завершения работы��6%    6E   �� *DateClosingRequest+  5"      $% Код оборудования��6%    6?   �� *IdEquipment+  5#      $0 *Код типа неисправности��6%    6E   �� *IdTypeErrorSupply+  5$      $' !Описание проблемы��6%    6@   �� *InfoAboutProblemRequest+  5%      $ Код клиента��6%    6:   �� *
IdClient+  5&      $ Код статуса��6%    6:   �� *IdStatusRequest+5(        5*   
   -   5        $��DG_Comments��5*   c   $��1��6   $��1��6    $ False��60   $ False��6G    5+        5,      $, &Комментарий к заявке��6%    6C   �� *CommentExecutorComment+  5-      $# Код исполнителя��6%    6>   �� *
IdExecutor+5/      50      51      