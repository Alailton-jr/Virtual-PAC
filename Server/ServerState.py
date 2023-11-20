#! /usr/bin/env python3

from flask import Flask, render_template
import pandas as pd
import psutil
import socket
import os

app = Flask(__name__)

app.template_folder = os.path.dirname(os.path.abspath(__file__))

def get_ip_address():
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    try:
        sock.connect(("8.8.8.8", 80))
        ip_address = sock.getsockname()[0]
    except socket.error:
        ip_address = '0.0.0.0'

    finally:
        sock.close()
    return ip_address

def get_running_processes():
    data = []
    for process in psutil.process_iter(attrs=['pid', 'name', 'username']):
        try:
            process_info = process.info
            pid = process_info['pid']
            name = process_info['name']
            username = process_info['username']
            data.append([pid, name, username])
        except (psutil.NoSuchProcess, psutil.AccessDenied, psutil.ZombieProcess):
            pass
    return pd.DataFrame(data, columns=["PID", "Name", "User"])

@app.route('/')
def display_processes():
    df = get_running_processes()
    table_data = df.to_html(classes='table table-bordered table-striped table-hover', index=False)
    columns = df.columns
    return render_template('table.html', table_data=table_data, columns=columns)

if __name__ == '__main__':
    app.run(host = get_ip_address(), port=8085, debug=False)