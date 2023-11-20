
.SILENT:

INSTALL_CMD :=
DISTRO :=

# Check with apt exists
APT_EXISTS := $(shell command -v apt-get 2>/dev/null)
ifeq ($(APT_EXISTS),)
	INSTALL_CMD := yum
	DISTRO := redhat
else
	INSTALL_CMD := apt-get
	DISTRO := debian
endif

PYTHON = python3
VENV = $(CURDIR)/vEnv
PIP = $(VENV)/bin/pip3

.PHONY: venv
venv: python_shebang

	@if ! dpkg -l |  grep -P 'python.*venv|venv.*python' >/dev/null; then \
		echo Installing Python VENV... \
		sudo $(INSTALL_CMD) update -y; \
		sudo $(INSTALL_CMD) install $(PYTHON)-venv; \
	fi

	@if [ ! -d $(VENV) ]; then \
		$(PYTHON) -m venv $(VENV); \
		echo "venv installed."; \
		$(PYTHON) -m ensurepip --upgrade; \
		echo "pip installed."; \
	fi

SHEBANG = \#!$(VENV)/bin/python3
.PHONY: python_shebang
python_shebang:
	@find $(Python_Code_Dir) -type f -name "*.py" -exec sed -i "1s@.*@$(SHEBANG)@" {} \;
	@find $(CURDIR) -type f -name "*.py" -exec sed -i "1s@.*@$(SHEBANG)@" {} \;

SERVICE_NAME = vIED.service
SYSTEM_SERVICE_PATH = /etc/systemd/system/
.PHONY: install_service
install_service:
	@cp $(CURDIR)/$(SERVICE_NAME) $(SYSTEM_SERVICE_PATH)
	@systemctl daemon-reload

.PHONY: install
install: venv install_service
	@echo "Updating Repository..."
ifeq ($(DISTRO),debian)
	@ sudo apt-get update -y
else ifeq ($(DISTRO),redhat)
	@ sudo yum makecache -y
endif

	@echo "Installing dependencies..."

	@if @ ! command -v gcc 2>/dev/null; then \
		@echo Installing gcc... \
		@ sudo $(INSTALL_CMD) install gcc; \
	fi
	@echo "gcc installed."

	@if ! dpkg -l | grep libfftw3-dev  >/dev/null; then \
		@echo Installing libfftw3-dev... \
		@ sudo $(INSTALL_CMD) install libfftw3-dev; \
	fi
	@echo "libfftw3-dev installed."

	@ $(PIP) install -r src/requirements.txt
	@echo "Python requirements Installed."

	@echo "Dependencies installed."


.PHONY: enable_service
enable_service:
	if [ -e /etc/systemd/system/vIED.service ]; then \
		systemctl enable vIED; \
		echo $(SERVICE_NAME) Enabled; \
	else \
		echo Creating $(SERVICE_NAME)...; \
		$(MAKE) install_service; \
		systemctl enable vIED; \
		echo $(SERVICE_NAME) Enabled; \
	fi

.PHONY: start_service
start_service:
	if [ -e /etc/systemd/system/vIED.service ]; then \
		systemctl start vIED; \
		echo $(SERVICE_NAME) Started; \
	else \
		echo Creating $(SERVICE_NAME)...; \
		$(MAKE) install_service; \
		systemctl enable vIED; \
		echo $(SERVICE_NAME) Started; \
	fi

.PHONY: stop_service
stop_service:
	if [ -e /etc/systemd/system/vIED.service ]; then \
		systemctl stop vIED; \
		echo $(SERVICE_NAME) Stoped; \
	else \
		echo Creating $(SERVICE_NAME)...; \
		$(MAKE) install_service; \
		systemctl stop vIED; \
		echo $(SERVICE_NAME) Stoped; \
	fi

.PHONY: disable_service
disable_service:
	if [ -e /etc/systemd/system/vIED.service ]; then \
		systemctl disable vIED; \
		echo $(SERVICE_NAME) Disabled; \
	else \
		echo Creating $(SERVICE_NAME)...; \
		$(MAKE) install_service; \
		systemctl disable vIED; \
		echo $(SERVICE_NAME) Disabled; \
	fi

CC = gcc
CFLAGS = -Wno-incompatible-pointer-types
LDFLAGS = -lm -lpthread -lfftw3
C_Code_Dir = $(CURDIR)/src/C_Code
C_Code_Build_Dir = $(CURDIR)/src/C_Build
Python_Code_Dir = $(CURDIR)/src/Python_Code

IGNORED_SRCS = goose.c debugSniffer.c testSetup.c
IGNORED_SRCS_WITH_PATH = $(addprefix $(C_Code_Dir)/, $(IGNORED_SRCS))

SRCS = $(filter-out $(IGNORED_SRCS_WITH_PATH), $(wildcard $(C_Code_Dir)/*.c))
EXECUTABLES = $(patsubst $(C_Code_Dir)/%.c,%,$(SRCS))
PYTHON_FILES := $(wildcard $(Python_Code_Dir)/*.py)

.PHONY: build
build: $(C_Code_Build_Dir) $(EXECUTABLES)
	@chmod +x $(PYTHON_FILES)
	@echo "C Code compiled ended."

$(C_Code_Build_Dir):
	@mkdir $(C_Code_Build_Dir)

%: $(C_Code_Dir)/%.c
	@echo "Compiling $@..."
	@$(CC) $(CFLAGS) -o $(C_Code_Build_Dir)/$@ $< $(LDFLAGS)
	
.PHONY: clean
clean:
	@echo "Cleaning..."
	@for executable in $(EXECUTABLES); do \
	    rm -f $(C_Code_Build_Dir)/$$executable; \
	done

.PHONY: debug
debug: install_service
	echo Hello World!

	

    