#ifndef ARCHIV_H
#define ARCHIV_H

#include <QObject>
#include <QString>
#include <vector>

namespace Const {
class archiv;
extern int MainCounter;
extern int PackBayte;
extern QString filePath;
}

class archiv
{
public:
    archiv(QString filePath);
    void extract(std::__cxx11::string finalPath);

private:
    QString readFile(QString url);
    void writeFile(std::__cxx11::string file, std::__cxx11::string content);
    bool checkNextItems(int count, int byte, int index, QStringList* ls);
    std::__cxx11::string convertIntToChar(int value);
    std::vector<std::__cxx11::string> split(std::__cxx11::string sequence, char sep);
    void replaceString(std::string *str, const std::__cxx11::string search, const std::__cxx11::string replace);
    std::string correctionPath(std::string file);
    void createDirectory(std::string path);

signals:

public slots:
};

#endif // ARCHIV_H
