#include "archiv.h"
#include <QFile>
#include <QTextStream>
#include <QStringList>
#include <QString>
#include <QObject>
#include <fstream>
#include <QDebug>
#include <iostream>
#include <vector>
#include <string>
#include <QDir>
#include <sys/stat.h>
#include <sys/types.h>

QString Const::filePath = "";
int Const::MainCounter = 128;
int Const::PackBayte = 45;

archiv::archiv(QString filePath)
{
    Const::filePath = filePath;
}

void archiv::extract(std::string finalPath)
{
    if (QFile::exists(Const::filePath))
    {
        std::string final("");

        QString currentContent(this->readFile(Const::filePath));
        // Extract this content
        QStringList values = currentContent.split("|");
        std::vector<std::string> bytes;
        bytes.push_back("");

        int s = 0;

        for (int i = 0; i <= values.length() - 1; i++)
        {
            int currentValue = values.at(i).toInt();            
            final.append(this->convertIntToChar(currentValue));
        }

        this->writeFile(finalPath, final);

        // Extract the file now!
        // We don't need to read it, because we have final as our content
        for (int i = 0; i <= values.length() - 1; i++)
        {
            if (this->checkNextItems(Const::MainCounter, Const::PackBayte, i, &values))
            {
                i += Const::MainCounter - 1;
                s++;
                bytes.push_back("");
            }
            else
            {
                bytes[s] += this->convertIntToChar(values.at(i).toInt());
            }
        }
        // Line just for checking the result debugging: qDebug() << QString::fromStdString(bytes[0]);
        std::vector<std::string> files = this->split(bytes[0], '<');
        if (bytes.size() == 1 && files.size() != 1)
            return;

        std::vector<std::string> rFiles, rFolders;
        int fCount = files.size() - 1;

        if (fCount == 0 || fCount == 1)
            rFiles = this->split(files[0], '|');
        if (fCount == 1)
            rFolders = this->split(files[1], ',');

        int count = 1;
        bool test = false;

        for (std::vector<std::string>::iterator folder = rFolders.begin(); folder != rFolders.end(); ++folder)
        {
            std::string currentFolder (this->replaceString(this->replaceString(*folder, ">" , ""), "<", ""));
            std::string path = this->correctionPath(currentFolder);

            if (path != "")
            {
                std::string nPath = finalPath + "/" + path;
                this->createDirectory(nPath);
            }
        }

        for (std::vector<std::string>::iterator it = bytes.begin(); it != bytes.end(); ++it)
        {
            if (!test)
            {
                test = true;
                continue;
            }
            // Files
            std::string fileName = this->correctionPath(rFiles[count - 1]);
            std::string filePath;

            if (fileName.at(0) == '\\')
                 filePath = this->correctionPath(fileName);
            else
                 filePath = fileName;

           std::string pathToWriteFile (std::string(finalPath + "/" + filePath));

            this->writeFile(pathToWriteFile, *it);
            count++;
        }
    }
}

std::vector<std::string> archiv::split(std::string sequence, char sep)
{
    std::vector<std::string> lstToReturn;
    for (size_t p = 0, q = 0; p != sequence.npos; p = q)
    {
        std::string s (std::string(sequence.substr(p+(p!=0), (q =sequence.find(sep, p+1))-p-(p!=0))));
        if (!s.empty())
            lstToReturn.push_back(s);
    }
    return lstToReturn;
}

std::string archiv::convertIntToChar(int value)
{
    char currentChar = value;
    std::string toAppend("");
    toAppend += (char)(currentChar);
    return toAppend;
}

void archiv::createDirectory(std::string path)
{
    std::vector<std::string> path_ex = this->split(path, '/');
    std::string startPath;

    for (int i = 0; i <= path_ex.size() - 1; i++)
    {
        startPath += path_ex.at(i);
        QDir currentDir (QString::fromStdString(startPath));
        if (!currentDir.exists())
        {
            QString commandForCreation ("mkdir ");
            commandForCreation.append("\"");
            commandForCreation.append(QString::fromStdString(startPath));
            commandForCreation.append("\"");
            system(commandForCreation.toLocal8Bit());
        }
        startPath += "/";
    }

}

std::string archiv::replaceString(std::string str, const std::string& search, const std::string& replace)
{
    size_t pos = 0;
    while ((pos = str.find(search, pos)) != std::string::npos)
    {
        str.replace(pos, search.length(), replace);
        pos += replace.length();
    }
    return str;
}

bool archiv::checkNextItems(int count, int byte, int index, QStringList *ls)
{
    bool check = false;
    int x = 0;

    for (int i = index; i <= index + count - 1; i++)
    {
        if (i >= ls->length() - 1)
            return false;

        char left = ls->at(i).toInt();
        char right = byte;

        if (left == right)
        {
            if (x == count)
                return true;
            check = true;
            x++;
        }
        else
            return false;
    }

    return check;
}

std::string archiv::correctionPath(std::string file)
{
    if (file == "")
        return "";
    std::string outputString;

    for (uint i = 1; i <= file.length() - 1; i++)
        outputString += file.at(i);

    outputString = this->replaceString(outputString, "\\", "/");
    return outputString;
}

void archiv::writeFile(std::string file, std::string content)
{
    std::ofstream os(file.c_str());
    os << content;
    os.close();
}

QString archiv::readFile(QString url)
{
    // Read file:
    QFile file(url);
    file.open(QIODevice::ReadOnly | QIODevice::Text);

    QTextStream in(&file);
    in.setCodec("UTF-8");

    return in.readAll();
}
