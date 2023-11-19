from tensorflow.keras.preprocessing.image import img_to_array
from tensorflow.keras.models import load_model
from PIL import Image
import numpy as np

def load_and_prepare_image(base64_string):

    img_data = base64.b64decode(base64_string)
    img = Image.open(io.BytesIO(img_data))
    img = img.convert('RGB')

    yimg_array = image.img_to_array(img)

    img_array /= 255.0

    img_batch = np.expand_dims(img_array, axis=0)
    return img_batch

def predict_image(model, base64_string):
    img = load_and_prepare_image(file_path)
    prediction = model.predict(img)

    predicted_class_index = np.argmax(prediction, axis=1)
    return predicted_class_index, prediction

def main(base64_string):
    model = load_model('./my_model.h5')

    predicted_class_index, prediction = predict_image(model, base64_string)
    
    return predicted_class_index;