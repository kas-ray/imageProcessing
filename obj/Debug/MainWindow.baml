   M S B A M L   `   `   `  ����C  ?lab1sem2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:lab1sem2lab1sem2  5   	       lab1sem2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:lab1sem2     5   	     � Title$  
MainWindow��5   	   $	��650��6   $
��1050��6)   ���#1F2023
������� � WindowStartupLocation$ CenterScreen=�5	   1    � 	Resources 5
         Button.MouseOver.Background �� Color  Info �� 
TargetType �� Property �� Value 
 Select  	Operation �� Triggers P� Property	 P� Value
 �� 	Resources 	 Enter  EnterMatrix 	 Check��%�  &	        &	      &	 �     &	 �    &	 �    &	 q    &	     �� 5   
   $ Green��6>   �� 5   
   " ��6   .�5      ��  � ��$ 16(�6)   �� 5       � ��$ 
10,10,10,0q�6'   �� 5       � ��$	 132��6&   �� 5       �@J�Opacity$	 0.9Q�6(   5      �� 5   
   " ��6   .�5      ��  � ��$ 26��6'   �� 5       � ��$
 0,10q�6'   �� 5       � ��$ 90��6&   �� 5       � �� �#1F20236+   5      �� 5   
   " ��6"    5      P� 5       �@J�IsMouseOver	 . . �5      ��  � �� �#BFBDE963   �� 5       � �� �#383B4063   5       .�5"      ��  � ��$	 210��6&   �� 5#       � ��$ 44��6'   �� 5$       � ��$ 16(�6)   �� 5%       � ��$ 10q�6'   �� 5&       � �� �#383B406,   �� 5'       � �� �#383B406+   �� 5(       � ��
 �����6+   
 5)      ���� 5*      '��       " ��6   .�5+      ��  �@��CornerRadius$ 3��65   5,      5-      5/      �� 51   
   " ��6   .�52      ��  � ��$ 90��6&   �� 53       � ��$ 26��6'   �� 54       � ��$
 0,10q�6'   55      �� 57   
   " ��6$   .�58      ��  � �� �#383B406+   �� 5:       � ��
 �����6+   �� 5;       � ��$ 16(�6)   �� 5<       � ��
 �����6,   �� 5=       �@��AcceptsReturn . 6.   �� 5>       � ��$	 194��6'   �� 5?      % �@��HorizontalScrollBarVisibility$ Visible=�6>   �� 5@      # �@��VerticalScrollBarVisibility$ Visible=�6<   5A      �� 5C   
   " ��6   .�5D      ��  � ��$ 33,15,33,10q�6'   5E      5G      .��5I      �  � RowDefinitions 5J   
   �� 5K      $��1* �6   �� 5L      $��1* �6    � ColumnDefinitions 5O   
   �� 5P      $��1* �6   �� 5Q      $��0.95* �6   �� 5R      $��0.95* �6   �� 5S      $��1* �6   .+�5V   
   �� $��3��6   .��5W      �� .+�5X      ��  �� Orientation$ 
Horizontal=�6   .+�5Y      �� $#��Первая картинка��6    #��[ 67   �� 5Z      -   5         ��Name$ SelectImage1��5Z      #��[ 6^   .@�5[      �� $��img1.jpg��6$   �� 5\      $��img2.jpg��6$   �� 5]      $��img3.jpg��6$   �� 5^      $��img4.jpg��6$   �� 5_      $��img5.jpg��6$   �� 5`      $��img6.jpg��6$   5a      5b      �� 5d      $ 
Horizontal=�6   .+�5e      �� $#��Вторая картинка��6    #��[ 67   �� 5f      -   5        $ SelectImage2��5f      #��[ 6^    �� Text$  ��6~   .@�5g      �� $��img1.jpg��6$   �� 5h      $��img2.jpg��6$   �� 5i      $��img3.jpg��6$   �� 5j      $��img4.jpg��6$   �� 5k      $��img5.jpg��6$   �� 5l      $��img6.jpg��6$   5m      5n      �� 5p      $ 
Horizontal=�6   .+�5q      �� $��Выбор маски��6    #��[ 63   �� 5r      -   5        $ 
SelectMask��5r       �� SelectedIndex$ 0��63   #��[ 6E   .@�5s      �� $��круг��6$   �� 5t      $��квадрат��6$   �� 5u      $ ��прямоугольник��6$   5v      5w      �� 5y      $ 
Horizontal=�6   .+�5z      �� $��Выбор канала��6    #��[ 64   �� 5{      -   5        $ 	SelectRGB��5{      $ 0��62   #��[ 6D   .@�5|      �� $	��RGB��6$   �� 5}      $��R��6$   �� 5~      $��G��6$   �� 5      $��B��6$   �� 5�      $��RG��6$   �� 5�      $��RB��6$   �� 5�      $��GB��6$   5�      5�      �� 5�      $ 
Horizontal=�6    � ToolTip$� }Вводить числа только с запятой.
Сигма для Гаусса - только целое число��66   .+�5�      �� $��Чувств-ость/σ��6    #��[ 65   �� 5�      -   5         ��Name$ Sence��5�      #��[ 6-   $��3��6y   5�      �� 5�      $.��(Размер окна / R / фильтр��6    #��[ 6@   $	��186��6^   �� 5�      -   5        $ WinSize��5�      #��[ 6/   $��20��6z   $� �Размер окна, радиус для медианной
фильтрации и размытия по Гауссу,
параметр для фильтра��5�      �� 5�      $ 
Horizontal=�6   $d ^Построение гистограммы замедляет
работу программы��66   .+�5�      �� $%��Хочу гистограмму��6    #��[ 68   �� 5�      -   5         ��Name$ NeedGist��5�       d� 	IsChecked$ FalseY�61   #��[ 6C   5�      �� 5�      -   5        #��[ 5�      $��Фурье образ��6M   �� 5�      -	   5        #��[ 5�      $2��,Низкочастот. фильтрация��6L   �� 5�      -
   5        #��[ 5�      $4��.Высокочастот. фильтрация��6M   �� 5�      -   5        #��[ 5�      $(��"Размытие по Гауссу��6O   �� 5�      -   5        #��[ 5�      $-��'Медианная фильтрация��6K   �� 5�      -   5        #��[ 5�      $#��Метод Гаврилова��6M   �� 5�      -   5        #��[ 5�      $��Метод Отсу��6I   �� 5�      -   5        #��[ 5�      $��Метод Ниблека��6K   �� 5�      -   5        #��[ 5�      $��Метод Сауволы��6K   �� 5�      -   5        #��[ 5�      $��Метод Вульфа��6I   �� 5�      -   5        #��[ 5�      $&�� Метод Брэдли-Рота��6K   �� 5�      -   5        #��[ 5�      $)��#Попиксельная сумма��6L   �� 5�      -   5        #��[ 5�      $1��+Среднее арифметическое��6P   �� 5�      -   5        #��[ 5�      $��Минимум��6L   �� 5�      -   5        #��[ 5�      $��Максимум��6L   �� 5�      -   5        #��[ 5�      $��
Маска��6M   �� 5�      -   5        #��[ 5�      $#��Очистить график��6P   5�      5�      �� 5�   
   -   5         ��Name$ Image_1��5�      $��1��6!   �� 5�   
   -   5        $ Image_2��5�      $��2��6!   $��0��61   �� 5�   
   -   5        $ 	Image_Out��5�      $��1��6#   $��1��63   �� 5�   
   $��3��6   $��1��6!   $��1q�6.   
�������$	��256��5�      $	��256��6   $	��256��6*   $	��256��69   ���#383B40$< 6Я гистограмма или Фурье образ��5�      ._�5�      �� -   5        $ 
ImageGisto��5�      $	��256��6(   $	��256��64   $	��256��6A   $	��256��6P   5�      �� 5�   
   $��1��6   $��2��6"   $��Center=�62   $��Center=�6M   .+�5�      �� -   5        $ MyMatrix��5�      #��[ 6(   $l fЯ матрица. Вводите все аккуратно,
иначе обижусь и вылечу��5�      $2��,-1 -1 -1 -1 -1
-1 -1 15 -1 -1
-1 -1 -1 -1 -1��5�      �� 6b   -   5        #��[ 5�   i   $'��!Применить матрицу��5�      5�      �� 5�   
   -   5         ��Name$ Graphic��5�      $��3��6"   $��0��62   $	��255��6?   $	��255��6K   $	��255��5�      $	��255��6     J� 	AllowDrop . ���#383B40$r lЯ график для преобразования картинки.
Нажмите чтобы начать��5�   &   .+�5�      �� $	��255��6   $	��255��6$   
�������$��1��6?   5�      5�      5�      