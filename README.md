# ImageDownsizer

ImageDownsizer is a simple Windows Forms application that allows users to downsize images.

|         |10%      | 50%      | 80%|
|--|--|--|--|
Sequential| 9 346ms | 42 559ms | 68 194ms|
Parallel  | 219ms | 618ms | 965ms|

Times masured from the application when testing with AG1121.jpg... As expected the parallel downsizing is quicker.
