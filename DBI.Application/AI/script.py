from tensorflow.keras.preprocessing.image import img_to_array
from tensorflow.keras.models import load_model
from PIL import Image
import numpy as np
import base64
import io

def load_and_prepare_image(base64_string):
    img_data = base64.b64decode(base64_string)
    img = Image.open(io.BytesIO(img_data))
    img = img.convert('RGB')

    img_array = img_to_array(img)

    img_array /= 255.0

    img_batch = np.expand_dims(img_array, axis=0)
    return img_batch

def predict_image(model, base64_string):
    img = load_and_prepare_image(base64_string)
    prediction = model.predict(img)

    predicted_class_index = np.argmax(prediction, axis=1)
    return predicted_class_index, prediction

def main(base64_string):
    #model = load_model(r'E:\Codes\DogBreedIdentification\Server\DBI.Application\AI\my_model.h5')
    model = load_model(r'./my_model.h5')

    predicted_class_index, prediction = predict_image(model, base64_string['base64_string'])
    
    return predicted_class_index;