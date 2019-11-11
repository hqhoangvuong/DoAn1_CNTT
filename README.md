# TensorGram

A TensorFlow's visualization toolkit (Like TensorBoard or Netron)
![alt text](https://www.upsieutoc.com/images/2019/11/10/01.png)
## Installation

It's a portable software no need to install, but you need to make sure your computer has been install the latest version of .NET Framework (Now is 4.7)


## Usage

```python
x = layers.Activation(activation = 'relu', name = 'conv1')(data)												
x = layers.Add(name='add1')(data)												
x = layers.Average(name='average1')(data)												
x = layers.AvgPool2D((3,4),(5,6),padding = 'vaild', data_format = 'channels_last', name = 'avgpool2d_1')(data)												
x = layers.BatchNormalization(4, 12.344, 'false', 'true', name = 'batchnormalization1')(data)												
x = layers.Concatenate(13, name = 'conactenate1')(data)												
x = layers.Dense(15, 'linear', name = 'dense1')(data)												
x = layers.Dropout(0.22332, 3, 13, name = 'dropout1')(data)												
x = layers.MaxPool2D((3,4),(5,6), padding = 'vaild', data_format = 'channels_last', name = 'maxpool2d_1')(data)												
x = layers.Softmax(3, name = 'softmax1')(data)	
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
