import numpy as np  # linear algebra
import pandas as pd  # data processing, CSV file I/O (e.g. pd.read_csv)
# Input data files are available in the read-only "../input/" directory
# For example, running this (by clicking run or pressing Shift+Enter) will list all files under the input directory
import matplotlib.pyplot as plt
import os

for dirname, _, filenames in os.walk('../../Jupyter/input'):
    for filename in filenames:
        print(os.path.join(dirname, filename))

content = pd.read_csv("../../Jupyter/input/netflix_titles.csv")
plt.title = 'Hoeveelheid Films en Series'
plt.pie(content.type.value_counts())
plt.show()